using EPVV_CBR_RU.Types.Enums;

namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация задачи из справочника (десериализуется)
    /// </summary>
    public class GuideTask
    {
        /// <summary>
        /// Код задачи (по справочнику задач в формате "Zadacha_*", где Zadacha_ - неизменная часть, * - число/набор символов определяющий порядковый номер/обозначение задачи), используется для идентификации задачи
        /// </summary>
        public string Code { get; set; } = default!;
        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Направление обмена по задаче
        /// </summary>
        public DirectionExchangeType Direction { get; set; }
        /// <summary>
        /// Признак возможности отправки связанных сообщений
        /// </summary>
        public bool AllowLinkedMessages { get; set; }
        /// <summary>
        /// Признак возможности отправки сообщений через Aspera
        /// </summary>
        public bool AllowAspera {  get; set; }
        /// <summary>
        /// Текстовое описание задачи
        /// </summary>
        public string? Description { get; set; }
    }
}
