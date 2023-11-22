namespace EPVV_CBR.RU.Models
{
    /// <summary>
    /// Репрезентация информации об отправителе (десериализация)
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// Индивидуальный номер налогоплательщика получателя
        /// </summary>
        public string Inn { get; set; } = default!;
        /// <summary>
        /// Основной государственный регистрационный номер получателя
        /// </summary>
        public string Ogrn { get; set; } = default!;
        /// <summary>
        /// Банковский идентификационный код получателя
        /// </summary>
        public string Bik { get; set; } = default!;
        /// <summary>
        /// Регистрационный номер КО – получателя по КГРКО
        /// </summary>
        public string RegNum { get; set; } = default!;
        /// <summary>
        /// Номер филиала КО – получателя по КГРКО
        /// </summary>
        public string DivisionCode { get; set; } = default!;
    }
}
