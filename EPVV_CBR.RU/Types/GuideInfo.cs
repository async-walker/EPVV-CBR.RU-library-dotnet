using System.Text.Json.Serialization;

namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о справочнике (десериализуется)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="text"></param>
    /// <param name="date"></param>
    [method: JsonConstructor]
    public class GuideInfo(string id, string text, string date)
    {
        /// <summary>
        /// Уникальный идентификатор справочника, используется для идентификации задачи в формате GUID
        /// </summary>
        public string Id { get; set; } = id;
        /// <summary>
        /// Текстовое наименование справочника
        /// </summary>
        public string Text { get; set; } = text;
        /// <summary>
        /// Дата последнего обновления справочника
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Parse(date);
    }
}
