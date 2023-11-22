using EPVV_CBR.RU.Data.Enums;
using EPVV_CBR.RU.Extensions;
using EPVV_CBR.RU.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace EPVV_CBR.RU
{
    /// <summary>
    /// Реализация интерфейса сервиса взаимодествия с ЕПВВ
    /// </summary>
    public class EpvvService : IEpvvService
    {
        private readonly EpvvServiceOptions _options;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Инициализация сервиса взаимодействия с ЕПВВ
        /// </summary>
        /// <param name="options">Настройки сервиса</param>
        /// <param name="httpClient">Экземпляр HttpClient</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EpvvService(EpvvServiceOptions options, HttpClient? httpClient = default)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? new HttpClient() { BaseAddress = new Uri(_options.BaseAddress) };
        }

        /// <inheritdoc/>
        public void Dispose() => _httpClient.Dispose();

        /// <inheritdoc/>
        public async Task<ResponseMessageBody> CreateDraftMessage(RequestMessageBody messageBody)
        {
            var data = JsonConvert.SerializeObject(messageBody);
            var content = new StringContent(data);
            var endpoint = "messages";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials, 
                endpoint: endpoint,
                method: HttpMethod.Post,
                content: content,
                contentType: ContentType.ApplicationJson);

            if (!response.IsSuccessStatusCode)
                await response.NotSuccesStatusCodeCatcher(
                    "При создании черновика сообщения произошла ошибка!");

            var message = await response.ReadContent();
            var responseMessageBody = message.DeserializeFromJson<ResponseMessageBody>();

            return responseMessageBody;
        }
        
        /// <inheritdoc/>
        public async Task<List<SessionInfo>> CreateUploadSessions(ResponseMessageBody messageResponse)
        {
            var sessionsInfo = new List<SessionInfo>();

            foreach (var file in messageResponse.Files)
            {
                var messageSession = new MessageSession(messageResponse.Id, file.Id);

                var endpoint = $"messages/{messageResponse.Id}/files/{file.Id}/createUploadSession";
                var data = messageSession.SerializeToJson();
                var content = new StringContent(data);

                var response = await _httpClient.GetResponse(
                    credentials: _options.Credentials,
                    endpoint: endpoint,
                    method: HttpMethod.Post, 
                    content: content,
                    contentType: ContentType.ApplicationJson);

                if (!response.IsSuccessStatusCode)
                    await response.NotSuccesStatusCodeCatcher(
                        "При создании сессии отправки файлов произошла ошибка!");

                var message = await response.ReadContent();
                var sessionInfo = message.DeserializeFromJson<SessionInfo>();

                sessionInfo!.MessageFile = file;

                sessionsInfo.Add(sessionInfo);
            }

            return sessionsInfo;
        }

        /// <inheritdoc/>
        public async Task<List<UploadedFile>> UploadFiles(List<SessionInfo> sessions, string folderPath)
        {
            var uploadedFiles = new List<UploadedFile>();

            foreach (var session in sessions)
            {
                var endpoint = session.UploadUrl.RemoveBaseAddressForEndpoint(_httpClient.BaseAddress!);

                var path = @$"{folderPath}\{session.MessageFile.Name}";
                var byteContent = await File.ReadAllBytesAsync(path);

                using (var content = new ByteArrayContent(byteContent))
                {
                    var contentLength = session.MessageFile.Size;
                    var contentRange = new ContentRangeHeaderValue(0, contentLength - 1, contentLength);

                    var response = await _httpClient.GetResponse(
                        credentials: _options.Credentials,
                        endpoint: endpoint,
                        method: HttpMethod.Put,
                        content: content,
                        contentType: ContentType.ApplicationOctetStream,
                        contentLength: contentLength,
                        contentRange: contentRange);

                    if (!response.IsSuccessStatusCode)
                        await response.NotSuccesStatusCodeCatcher(
                            "Во время загрузки файлов на сервер произошла ошибка!");

                    var message = await response.ReadContent();
                    var uploadedFile = message.DeserializeFromJson<UploadedFile>();

                    uploadedFiles.Add(uploadedFile!);
                }
            }

            return uploadedFiles;
        }

        /// <inheritdoc/>
        public async Task ConfirmSendMessage(string messageId)
        {
            var endpoint = $"messages/{messageId}";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Post);

            if (!response.IsSuccessStatusCode)
                await response.NotSuccesStatusCodeCatcher(
                    "При подтверждении отправки сообщения произошла ошибка!");
        }

        /// <inheritdoc/>
        public async Task<List<MessageInfo>> GetMessagesInfoByParameters(string[] parametersQuery)
        {
            var endpoint = $"messages?" + string.Join("&", parametersQuery);
            
            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Get);

            if (!response.IsSuccessStatusCode)
                await response.NotSuccesStatusCodeCatcher(
                    "При поиске сообщений по параметрам произошла ошибка!");

            var message = await response.ReadContent();
            var messagesInfo = message.DeserializeFromJson<List<MessageInfo>>();
            
            return messagesInfo;
        }

        /// <inheritdoc/>
        public async Task<List<string>> DownloadFilesFromRepository(MessageInfo messageInfo, string directory)
        {
            var filesPath = new List<string>();

            foreach (var file in messageInfo.Files)
            {
                var endpoint = $"messages/{messageInfo.Id}/files/{file.Id}/download";

                var response = await _httpClient.GetResponse(
                    credentials: _options.Credentials,
                    endpoint: endpoint,
                    method: HttpMethod.Get);

                if (!response.IsSuccessStatusCode)
                    await response.NotSuccesStatusCodeCatcher(
                        "При скачивании файлов из репозитория произошла ошибка!");

                var path = @$"{directory}\{file.Name}";

                await response.Content.WriteStreamContentToFile(path);

                filesPath.Add(path);
            }

            return filesPath;
        }

        /// <inheritdoc/>
        public async Task<MessageInfo> GetMessageInfoById(string messageId)
        {
            var endpoint = $"messages/{messageId}";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Get);

            if (!response.IsSuccessStatusCode)
                await response.NotSuccesStatusCodeCatcher("При получении информации о сообщения по его ID произошла ошибка!");

            var message = await response.ReadContent();
            var messageInfo = message.DeserializeFromJson<MessageInfo>();
            
            return messageInfo;
        }
    }
}
