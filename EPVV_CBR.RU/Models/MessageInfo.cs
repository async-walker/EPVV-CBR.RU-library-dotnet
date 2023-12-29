using EPVV_CBR_RU.Enums;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Репрезентация информации о сообщении
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// Уникальный идентификатор сообщения в формате UUID
        /// </summary>
        public string Id { get; set; } = default!;
        /// <summary>
        /// Идентификатор корреляции сообщения в формате UUID
        /// </summary>
        public string? CorrelationId { get; set; }
        /// <summary>
        /// Идентификатор группы сообщений в формате UUID
        /// </summary>
        public string? GroupId { get; set; }
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
        public string? Text { get; set; }
        /// <summary>
        /// Дата создания сообщения
        /// </summary>
        public string CreationDate { get; set; } = default!;
        /// <summary>
        /// Дата последнего изменения статуса сообщения
        /// </summary>
        public string? UpdatedDate { get; set; }
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
        public string? RegNumber { get; set; }
        /// <summary>
        /// Общий размер сообщения в байтах
        /// </summary>
        public int TotalSize { get; set; }
        /// <summary>
        /// Отправитель сообщения (только для сообщений, отправляемых другими Пользователями)
        /// </summary>
        public Sender? Sender { get; set; }
        /// <summary>
        /// Файлы, включенные в сообщение
        /// </summary>
        public List<UploadedFile> Files { get; set; } = default!;
    }
}
