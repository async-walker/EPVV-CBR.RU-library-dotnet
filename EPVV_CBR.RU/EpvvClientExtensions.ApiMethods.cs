using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Models;
using EPVV_CBR_RU.Requests.Methods.GetMessagesInfo;
using EPVV_CBR_RU.Requests.Methods.SendMessages;
using System.Net;

namespace EPVV_CBR_RU
{
    /// <summary>
    /// Класс расширений для взаимодействия с методами API
    /// </summary>
    public static class EpvvClientExtensions
    {
        /// <summary>
        /// Создание черновика сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="task"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="files"></param>
        /// <param name="correlationId"></param>
        /// <param name="groupId"></param>
        /// <param name="receivers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<DraftMessage> CreateDraftMessageAsync(
            this IEpvvClient client,
            string task,
            string title,
            string text,
            List<DirectedFile> files,
            string? correlationId = default,
            string? groupId = default,
            List<Receiver>? receivers = default,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
                .MakeRequestAsync(
                    request: new CreateDraftMessageRequest(
                        task: task,
                        title: title,
                        text: text, 
                        files: files, 
                        correlationId: correlationId, 
                        groupId: groupId, 
                        receivers: receivers),
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Создание сессии загрузки файла
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<SessionInfo> CreateUploadSessionAsync(
            this IEpvvClient client,
            string messageId,
            string fileId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new CreateUploadSessionRequest(messageId, fileId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sessionInfo"></param>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<UploadedFile> UploadFileAsync(
            this IEpvvClient client,
            SessionInfo sessionInfo,
            Stream stream,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new UploadFileRequest(sessionInfo, stream),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Подтверждение отправки сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        public static async Task ConfirmSendMessage(
            this IEpvvClient client,
            string messageId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await client.ThrowIfNull()
                   .MakeRequestAsync(
                       request: new ConfirmSendMessageRequest(messageId),
                       cancellationToken)
                   .ConfigureAwait(false);
            }
            catch (RequestException ex)
            {
                if (ex.HttpStatusCode is HttpStatusCode.OK)
                    return;
                else throw;
            }
        }

        /// <summary>
        /// Поиск сообщений по фильтрам
        /// </summary>
        /// <param name="client"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<List<MessageInfo>> GetMessagesInfoByFiltersAsync(
            this IEpvvClient client,
            QueryFilters? filters = default,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetMessagesInfoByFiltersRequest(filters),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Поиск информации о сообщении по его ID
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<MessageInfo> GetMessageInfoByIdAsync(
            this IEpvvClient client,
            string messageId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetMessageInfoByIdRequest(messageId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Загрузка сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="directoryToSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> DownloadMessageAsync(
            this IEpvvClient client,
            string messageId,
            string directoryToSave,
            CancellationToken cancellationToken = default)
        {
            var path = Path.Combine(directoryToSave, $"{messageId}.zip");

            await client.DownloadFileAsync(
                endpoint: $"messages/{messageId}/download",
                destination: new FileStream(path, FileMode.OpenOrCreate),
                cancellationToken)
                .ConfigureAwait(false);

            return path;
        }
    }
}
