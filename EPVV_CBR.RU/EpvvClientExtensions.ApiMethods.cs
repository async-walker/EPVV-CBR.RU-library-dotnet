using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Requests.Methods.DeleteMessages;
using EPVV_CBR_RU.Requests.Methods.GetMessagesInfo;
using EPVV_CBR_RU.Requests.Methods.GetReferenceInfo;
using EPVV_CBR_RU.Requests.Methods.SendMessages;
using EPVV_CBR_RU.Types;
using EPVV_CBR_RU.Types.Enums;
using EPVV_CBR_RU.Types.Responses;

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
                        task, title, text, files, correlationId, groupId, receivers),
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
            CancellationToken cancellationToken = default) =>
        await client.ThrowIfNull()
            .MakeRequestAsync(
                request: new ConfirmSendMessageRequest(messageId),
                cancellationToken)
            .ConfigureAwait(false);

        /// <summary>
        /// Получение всех сообщений по фильтрам (не более 100 сообщений за один запрос)
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
        /// Получение данных о сообщении по его ID
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

            using var destStream = new FileStream(path, FileMode.OpenOrCreate);

            await client.DownloadFileAsync(
                path: $"messages/{messageId}/download",
                destination: destStream,
                cancellationToken)
                .ConfigureAwait(false);

            return path;
        }

        /// <summary>
        /// Получение данных о конкретном файле из сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<UploadedFile> GetMessageFileInfoAsync(
            this IEpvvClient client,
            string messageId,
            string fileId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetMessageFileInfoRequest(messageId, fileId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Скачивание конкретного файла из сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="fileId"></param>
        /// <param name="directoryToSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> DownloadFileFromMessageAsync(
            this IEpvvClient client,
            string messageId,
            string fileId,
            string directoryToSave,
            CancellationToken cancellationToken = default)
        {
            var message = await client.GetMessageFileInfoAsync(messageId, fileId, cancellationToken);

            var path = Path.Combine(directoryToSave, message.Name);

            using var destStream = new FileStream(path, FileMode.OpenOrCreate); 

            await client.DownloadFileAsync(
                path: $"messages/{messageId}/files/{fileId}/download",
                destination: destStream,
                cancellationToken)
                .ConfigureAwait(false);

            return path;
        }

        /// <summary>
        /// Получение данных о всех квитанциях на сообщение
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<List<Receipt>> GetReceiptsInfoAsync(
            this IEpvvClient client,
            string messageId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetReceiptsInfoRequest(messageId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Получение данных о квитанции на сообщение по его ID
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="receiptId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Receipt> GetReceiptByIdAsync(
            this IEpvvClient client,
            string messageId,
            string receiptId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetReceiptByIdRequest(messageId, receiptId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Получение данных о файле квитанции на сообщение
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="receiptId"></param>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<UploadedFile> GetReceiptFileInfoAsync(
            this IEpvvClient client,
            string messageId,
            string receiptId,
            string fileId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetReceiptFileInfoRequest(messageId, receiptId, fileId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Скачивание файла квитанции на сообщение
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="receiptId"></param>
        /// <param name="fileId"></param>
        /// <param name="directoryToSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> DownloadReceiptAsync(
            this IEpvvClient client,
            string messageId,
            string receiptId,
            string fileId,
            string directoryToSave,
            CancellationToken cancellationToken = default)
        {
            var receipt = await client.GetReceiptFileInfoAsync(
                messageId, receiptId, fileId, cancellationToken);

            var path = Path.Combine(directoryToSave, receipt.Name);

            using var destStream = new FileStream(path, FileMode.OpenOrCreate);

            await client.DownloadFileAsync(
                path: $"messages/{messageId}/receipts/{receiptId}/files/{fileId}/download",
                destination: destStream,
                cancellationToken)
                .ConfigureAwait(false);

            return path;
        }

        /// <summary>
        /// Удаление сообщения по его ID
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task DeleteMessageByIdAsync(
            this IEpvvClient client,
            string messageId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new DeleteMessageRequest(messageId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Удаление конкретного файла или отмена сессии отправки
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageId"></param>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task DeleteFileOrSessionAsync(
            this IEpvvClient client,
            string messageId,
            string fileId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new DeleteFileOrSessionRequest(messageId, fileId),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Получение справочника задач
        /// </summary>
        /// <param name="client"></param>
        /// <param name="directionExchange">Направление обмена по задаче (необязательно). Если параметр не указан, возвращается все задачи</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<List<GuideTask>> GetGuideTasksAsync(
            this IEpvvClient client,
            DirectionExchangeType? directionExchange = default,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetGuideTasksRequest(directionExchange),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Получение информации о своем профиле
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ProfileInformation> GetMyProfileAsync(
            this IEpvvClient client,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetMyProfileRequest(),
                   cancellationToken)
               .ConfigureAwait(false);
    }
}
