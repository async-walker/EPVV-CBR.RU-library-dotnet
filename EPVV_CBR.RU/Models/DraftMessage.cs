using EPVV_CBR_RU.Enums;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Репрезентация созданного черновика сообщения, полученного от API
    /// </summary>
    public class DraftMessage
    {
        /// <summary>
        /// Уникальный идентификатор сообщения в формате UUID [16]
        /// </summary>
        public string Id { get; set; } = default!;
        /// <summary>
        /// Идентификатор корреляции сообщения в формате UUID [16]
        /// </summary>
        public string CorrelationId { get; set; } = default!;
        /// <summary>
        /// Идентификатор группы сообщений в формате UUID [16]
        /// </summary>
        public string GroupId { get; set; } = default!;
        /// <summary>
        /// Тип сообщения 
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// Название сообщения
        /// </summary>
        public string Title { get; set; } = default!;
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; } = default!;
        /// <summary>
        /// Дата создания сообщения (ГОСТ ISO 8601-2001 по маске «yyyy-MM-dd’T’HH:mm:ss’Z’»)
        /// </summary>
        public string CreationDate { get; set; } = default!;
        /// <summary>
        /// Дата последнего изменения статуса сообщения (ГОСТ ISO 8601-2001 по маске «yyyy-MM-dd’T’HH:mm:ss’Z’»)
        /// </summary>
        public string UpdatedDate { get; set; } = default!;
        /// <summary>
        /// Статус сообщения 
        /// </summary>
        public MessageStatus Status { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string TaskName { get; set; } = default!;
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegNumber { get; set; } = default!;
        /// <summary>
        /// Общий размер сообщения в байтах
        /// </summary>
        public long TotalSize { get; set; }
        /// <summary>
        /// Информация об отправителе
        /// </summary>
        public Sender Sender { get; set; } = default!;
        /// <summary>
        /// Файлы, включенные в сообщение
        /// </summary>
        public List<UploadedFile> Files { get; set; } = default!;
        /// <summary>
        /// Квитанции, полученные в ответ на сообщение
        /// </summary>
        public List<Receipt> Receipts { get; set; } = default!;
    }
}
