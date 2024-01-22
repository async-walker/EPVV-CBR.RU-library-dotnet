using System.Text.Json.Serialization;

namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о технических оповещениях (десериализуется)
    /// </summary>
    [method: JsonConstructor]
    public class NotificationInfo(string text, string date)
    {
        /// <summary>
        /// Текст технического оповещения
        /// </summary>
        public string Text { get; set; } = text;
        /// <summary>
        /// Дата и время технического оповещения
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Parse(date);
    }
}
