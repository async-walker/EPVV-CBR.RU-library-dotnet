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
        public async Task<ResponseViewModel<ResponseMessageBody>> CreateDraftMessage(RequestMessageBody messageBody)
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

            var responseViewModel = new ResponseViewModel<ResponseMessageBody>(
                responseModel: responseMessageBody,
                statusCode: response.StatusCode,
                message: "Черновик сообщения успешно создан");

            return responseViewModel;
        }
        
        /// <inheritdoc/>
        public async Task<ResponseViewModel<List<SessionInfo>>> CreateUploadSessions(ResponseMessageBody messageResponse)
        {
            var uploadsData = new List<SessionInfo>();

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

                uploadsData.Add(sessionInfo);
            }

            var responseViewModel = new ResponseViewModel<List<SessionInfo>>(
                responseModel: uploadsData,
                statusCode: HttpStatusCode.Created,
                message: "Сессии для отправки файлов сообщения успешно созданы");

            return responseViewModel;
        }

        /// <inheritdoc/>
        public async Task<ResponseViewModel<List<UploadedFile>>> UploadFiles(List<SessionInfo> sessions, string folderPath)
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

            var responseViewModel = new ResponseViewModel<List<UploadedFile>>(
                responseModel: uploadedFiles,
                statusCode: HttpStatusCode.Created,
                message: "Файлы сообщения успешно загружены в репозиторий");

            return responseViewModel;
        }

        /// <inheritdoc/>
        public async Task<ResponseViewModel> ConfirmSendMessage(string messageId)
        {
            var endpoint = $"messages/{messageId}";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Post);

            if (!response.IsSuccessStatusCode)
                await response.NotSuccesStatusCodeCatcher(
                    "При подтверждении отправки сообщения произошла ошибка!");

            var responseViewModel = new ResponseViewModel(
                statusCode: response.StatusCode,
                message: "Подтверждение отправки сообщения завершено успешно");

            return responseViewModel;
        }

        /// <inheritdoc/>
        public async Task<ResponseViewModel<List<MessageInfo>>> GetMessagesInfoByParameters(string[] parametersQuery)
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
            var responseViewModel = new ResponseViewModel<List<MessageInfo>>(
                responseModel: messagesInfo,
                statusCode: response.StatusCode);

            return responseViewModel;
        }

        /// <inheritdoc/>
        public async Task<ResponseViewModel<List<string>>> DownloadFilesFromRepository(MessageInfo messageInfo, string directory)
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

            var responseViewModel = new ResponseViewModel<List<string>>(
                responseModel: filesPath,
                statusCode: HttpStatusCode.OK);

            return responseViewModel;
        }

        /// <inheritdoc/>
        public async Task<ResponseViewModel<MessageInfo>> GetMessageInfoById(string messageId)
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
            var responseViewModel = new ResponseViewModel<MessageInfo>(
                responseModel: messageInfo,
                statusCode: response.StatusCode);

            return responseViewModel;
        }
    }
}
