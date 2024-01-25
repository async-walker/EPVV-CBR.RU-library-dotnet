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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="task">Код задачи (по справочнику задач в формате «Zadacha_*", где Zadacha_ - неизменная часть, * - число/набор символов, определяющий порядковый номер/обозначение задачи), используется для идентификации задачи</param>
        /// <param name="title">Название сообщения, отображается в интерфейсе</param>
        /// <param name="text">Текст сообщения, отображается в интерфейсе</param>
        /// <param name="files">Файлы включенные в сообщение</param>
        /// <param name="correlationId">Идентификатор корреляции сообщения в формате GUID (необязательно, указывается для формирования ответного сообщения для потоков, поддерживаемых данную функциональность)</param>
        /// <param name="groupId">Идентификатор группы сообщений в формате GUID (необязательно, указывается для передачи информации о том, что сообщение является частью группы сообщений для потоков, поддерживаемых данную функциональность)</param>
        /// <param name="receivers">Получатели сообщения (необязательно, указывается для потоков адресной рассылки)</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Экземпляр <see cref="DraftMessage"/></returns>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID, полученный в качестве ответа при вызове метода <see cref="CreateDraftMessageAsync"/></param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID, полученный в качестве ответа при вызове метода <see cref="CreateDraftMessageAsync"/></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Экземпляр <see cref="SessionInfo"/></returns>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="sessionInfo">Экземпляр <see cref="SessionInfo"/>, полученный в качестве ответа при вызове метода <see cref="CreateUploadSessionAsync"/>></param>
        /// <param name="source">Поток источника <see cref="Stream"/>, содержащий данные загружаемого файла</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Экземпляр <see cref="UploadedFile"/></returns>
        public static async Task<UploadedFile> UploadFileAsync(
            this IEpvvClient client,
            SessionInfo sessionInfo,
            Stream source,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new UploadFileRequest(sessionInfo, source),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Подтверждение отправки сообщения
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID, полученный в качестве ответа при вызове метода <see cref="CreateDraftMessageAsync"/></param>
        /// <param name="cancellationToken">Токен отмены</param>
        public static async Task ConfirmSendMessageAsync(
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="filters">Онлайн-запрос критериев для поиска сообщений</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список <see cref="MessageInfo"/> по указанным фильтрам поиска</returns>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Экземпляр <see cref="MessageInfo"/></returns>
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
        /// Загрузка сообщения в пользовательскую директорию
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="directoryToSave">Директория на диске, куда следует сохранить архив с сообщением</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Путь к скачанному архиву с сообщением</returns>
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
        /// Загрузка сообщения в поток <see cref="Stream"/>
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="destination">Пользовательский поток, куда будут переданы данные сообщения в виде zip-архива</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public static async Task DownloadMessageAsync(
            this IEpvvClient client,
            string messageId,
            Stream destination,
            CancellationToken cancellationToken = default) =>
            await client.DownloadFileAsync(
                path: $"messages/{messageId}/download",
                destination,
                cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Получение данных о конкретном файле из сообщения
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Экземпляр <see cref="UploadedFile"/></returns>
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
        /// Скачивание конкретного файла из сообщения в пользовательскую директорию
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID</param>
        /// <param name="directoryToSave">Директория, куда следует сохранить файл</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Путь к скачанному файлу</returns>
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
        /// Скачивание конкретного файла из сообщения в поток <see cref="Stream"/>
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID</param>
        /// <param name="destination">Пользовательский поток, в который будет передан массив байтов данных сообщения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task DownloadFileFromMessageAsync(
            this IEpvvClient client,
            string messageId,
            string fileId,
            Stream destination,
            CancellationToken cancellationToken = default) =>
            await client.DownloadFileAsync(
                path: $"messages/{messageId}/files/{fileId}/download",
                destination,
                cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Получение данных о всех квитанциях на сообщение
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список <see cref="Receipt"/> на сообщение</returns>
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
        /// Получение данных об определенной квитанции на сообщение
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="receiptId">Уникальный идентификатор квитанции в формате GUID</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Экземпляр <see cref="Receipt"/></returns>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="receiptId">Уникальный идентификатор квитанции в формате GUID</param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>"Экземпляр <see cref="UploadedFile"/></returns>
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
        /// Скачивание файла квитанции на сообщение в пользовательскую директорию
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="receiptId">Уникальный идентификатор квитанции в формате GUID</param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID</param>
        /// <param name="directoryToSave">Директория, куда следует сохранить файл</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Путь к скачанному файлу квитанции</returns>
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
        /// Скачивание файла квитанции на сообщение в поток <see cref="Stream"/>
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="receiptId">Уникальный идентификатор квитанции в формате GUID</param>
        /// <param name="fileId">Уникальный идентификатор файла в формате GUID</param>
        /// <param name="destination"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task DownloadReceiptAsync(
            this IEpvvClient client,
            string messageId,
            string receiptId,
            string fileId,
            Stream destination,
            CancellationToken cancellationToken = default) =>
            await client.DownloadFileAsync(
                path: $"messages/{messageId}/receipts/{receiptId}/files/{fileId}/download",
                destination,
                cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Удаление сообщения по его ID
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="cancellationToken">Токен отмены</param>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате GUID</param>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken">Токен отмены</param>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="directionExchange">Направление обмена по задаче (необязательно). Если параметр не указан, возвращаются все задачи</param>
        /// <param name="cancellationToken">Токен отмены</param>
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
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task<ProfileInfo> GetProfileInfoAsync(
            this IEpvvClient client,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetProfileInfoRequest(),
                   cancellationToken)
               .ConfigureAwait(false);

        /// <summary>
        /// Получение информации о квоте профиля
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task<QuotaInfo> GetProfileQuotaAsync(
            this IEpvvClient client,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
                .MakeRequestAsync(
                    request: new GetProfileQuotaRequest(),
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Получение информации о технических оповещениях
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task<List<NotificationInfo>> GetNotificationsAsync(
            this IEpvvClient client,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
                .MakeRequestAsync(
                    request: new GetNotificationsRequest(),
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Получение списка справочников
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task<List<GuideInfo>> GetGuideListAsync(
            this IEpvvClient client,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
                .MakeRequestAsync(
                    request: new GetGuideListRequest(),
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Получение записей конкретного справочника
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="guideId"></param>
        /// <param name="page"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task<GuideRecordsInfo> GetGuideRecordsAsync(
            this IEpvvClient client,
            string guideId,
            int? page = default,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
                .MakeRequestAsync(
                    request: new GetGuideRecordsRequest(guideId, page),
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Скачивание справочника в пользовательскую директорию
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="guideId">Уникальный идентификатор справочника в формате GUID</param>
        /// <param name="directoryToSave">Директория, куда нужно сохранить архив</param>
        /// <param name="guideName">Название справочника (без расширений файла)</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task DownloadGuideAsync(
            this IEpvvClient client,
            string guideId,
            string directoryToSave,
            string guideName,
            CancellationToken cancellationToken = default)
        {
            var path = Path.Combine(directoryToSave, $"{guideName}.zip");

            using var destStream = new FileStream(path, FileMode.OpenOrCreate);

            await client.DownloadFileAsync(
                    path: $"dictionares/{guideId}/download",
                    destination: destStream,
                    cancellationToken)
                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Скачивание справочника в поток <see cref="Stream"/>
        /// </summary>
        /// <param name="client">Клиент для запроса к ЕПВВ</param>
        /// <param name="guideId">Уникальный идентификатор справочника в формате GUID</param>
        /// <param name="destination">Поток назначения, в который будет передан двоичный поток вида application/octet-stream, содержащий zip-архив с двумя файлами в формате xml</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public static async Task DownloadGuideAsync(
            this IEpvvClient client,
            string guideId,
            Stream destination,
            CancellationToken cancellationToken = default)
        {
            await client.DownloadFileAsync(
                    path: $"dictionaries/{guideId}/download",
                    destination,
                    cancellationToken)
                    .ConfigureAwait(false);
        }
    }
}
