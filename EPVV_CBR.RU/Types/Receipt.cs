using EPVV_CBR_RU.Types.Enums;

namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация квитанции, полученной в ответ на сообщение
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Уникальный идентификатор файла
        /// </summary>
        public string Id { get; set; } = default!;
        /// <summary>
        /// Время получения квитанции
        /// </summary>
        public string ReceiveTime { get; set; } = default!;
        /// <summary>
        /// Время из самой квитанции
        /// </summary>
        public string StatusTime { get; set; } = default!;
        /// <summary>
        /// Состояние обработки сообщения
        /// </summary>
        public MessageStatus Status { get; set; }
        /// <summary>
        /// Дополнительная информация из квитанции
        /// </summary>
        public string Message { get; set; } = default!;
        /// <summary>
        /// Файлы, включенные в квитанцию
        /// </summary>
        public List<UploadedFile> Files { get; set; } = default!;
    }
}
