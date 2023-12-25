using EPVV_CBR_RU.Enums;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Репрезентация квитанции, полученной в ответ на сообщение
    /// </summary>
    public class Receipt
    {
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
        public List<ReceivedFile> Files { get; set; } = default!;
    }
}
