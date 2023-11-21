using EPVV_CBR.RU.Extensions;
using System.Net.Http.Headers;

namespace EPVV_CBR.RU
{
    public class WebRequestCB : IWebRequestCB
    {
        private readonly WebRequestOptions _options;

        private readonly HttpClient _httpClient;

        public WebRequestCB(WebRequestOptions options, HttpClient? httpClient = default)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? new HttpClient() { BaseAddress = new Uri(_options.BaseAddress) };
        }

        public void DisposeClient() => _httpClient.Dispose();

        public async Task<ResponseViewModel<ResponseMessageBody>> PostMessages(string data)
        {
            var content = new StringContent(data);
            var endpoint = "messages";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials, 
                endpoint: endpoint,
                method: HttpMethod.Post,
                content: content,
                contentType: ContentType.ApplicationJson);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"При создании черновика произошла ошибка (Код {(int)response.StatusCode})");

            var message = await response.ReadHttpResponseMessage();
            var responseMessageBody = message.DeserializeFromJson<ResponseMessageBody>();

            var template = $"Черновик сообщения {responseMessageBody!.Id} успешно создан [{DateTime.Now}]\n\n";

            var responseViewModel = new ResponseViewModel<ResponseMessageBody>(
                responseModel: responseMessageBody,
                statusCode: response.StatusCode,
                message: template);

            return responseViewModel;
        }

        public async Task<ResponseViewModel<List<SessionInfo>>> PostMessagesCreateUploadSession(ResponseMessageBody messageResponse)
        {
            var uploadsData = new List<SessionInfo>();
            var template = string.Empty;

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
                    throw new HttpRequestException($"При создании сессии произошла ошибка (Код {(int)response.StatusCode})");

                var message = await response.ReadHttpResponseMessage();
                var sessionInfo = message.DeserializeFromJson<SessionInfo>();
                sessionInfo!.MessageFile = file;

                uploadsData.Add(sessionInfo);

                template += ($"Сессия для отправки {file.Name} успешно создана\n");
            }

            template += $"Сессии для отправки файлов сообщения успешно созданы [{DateTime.Now}]\n\n";

            var responseViewModel = new ResponseViewModel<List<SessionInfo>>(
                responseModel: uploadsData,
                statusCode: System.Net.HttpStatusCode.Created,
                message: template);

            return responseViewModel;
        }

        public async Task<ResponseViewModel<List<UploadedFile>>> PutMessages(List<SessionInfo> sessions, string folderPath)
        {
            var uploadedFiles = new List<UploadedFile>();
            var template = string.Empty;

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
                        throw new HttpRequestException($"При загрузке файла в репозиторий произошла ошибка (Код {(int)response.StatusCode})");

                    var message = await response.ReadHttpResponseMessage();
                    var uploadedFile = message.DeserializeFromJson<UploadedFile>();

                    uploadedFiles.Add(uploadedFile!);

                    template += ($"Файл {session.MessageFile.Name} успешно загружен в репозиторий\n");
                }
            }

            template += $"Файлы сообщения успешно загружены в репозиторий [{DateTime.Now}]\n\n";

            var responseViewModel = new ResponseViewModel<List<UploadedFile>>(
                responseModel: uploadedFiles,
                statusCode: System.Net.HttpStatusCode.Created,
                message: template);

            return responseViewModel;
        }

        public async Task<ResponseViewModel<object>> PostConfirmSendMessages(string messageId)
        {
            var endpoint = $"messages/{messageId}";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Post);

            if (!response.IsSuccessStatusCode && (int)response.StatusCode != 500)
                throw new HttpRequestException($"Сообщение не было отправлено на сервер (Код {(int)response.StatusCode})");

            var template = $"Сообщение {messageId} успешно отправлено на сервер ЦБ [{DateTime.Now}]\n";
            
            if ((int)response.StatusCode is 500)
                template = "Сообщение имеет статусный код '500', вероятней всего, сервер не отвечает, но сообщение загружено.. проверка статуса\n";

            var responseViewModel = new ResponseViewModel<object>(
                responseModel: null,
                statusCode: response.StatusCode,
                message: template);

            return responseViewModel;
        }

        public async Task<ResponseViewModel<List<MessageInfo>>> GetMessagesInfoByParameters(string[] parametersQuery)
        {
            var endpoint = $"messages?" + string.Join("&", parametersQuery);
            
            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Get);

            var message = await response.ReadHttpResponseMessage();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Некорректный ответ сервера (Код {(int)response.StatusCode})");

            var messagesInfo = message.DeserializeFromJson<List<MessageInfo>>();

            var responseViewModel = new ResponseViewModel<List<MessageInfo>>(
                responseModel: messagesInfo,
                statusCode: response.StatusCode);

            return responseViewModel;
        }

        public async Task<ResponseViewModel<List<string>>> GetDownloadFilesFromMessage(MessageInfo messageInfo, string directory)
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
                    throw new HttpRequestException($"Некорректный ответ сервера: (Код {(int)response.StatusCode})");

                var path = @$"{directory}\{file.Name}";

                await response.Content.WriteStreamContentToFile(path);

                filesPath.Add(path);
            }

            var responseViewModel = new ResponseViewModel<List<string>>(
                responseModel: filesPath,
                statusCode: System.Net.HttpStatusCode.OK);

            return responseViewModel;
        }

        public async Task<ResponseViewModel<MessageInfo>> GetMessageInfoById(string messageId)
        {
            var endpoint = $"messages/{messageId}";

            var response = await _httpClient.GetResponse(
                credentials: _options.Credentials,
                endpoint: endpoint,
                method: HttpMethod.Get);

            var message = await response.ReadHttpResponseMessage();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Некорректный ответ сервера: (Код: {(int)response.StatusCode})");

            var messageInfo = message.DeserializeFromJson<MessageInfo>();

            var responseViewModel = new ResponseViewModel<MessageInfo>(
                responseModel: messageInfo,
                statusCode: System.Net.HttpStatusCode.OK);

            return responseViewModel;
        }
    }
}
