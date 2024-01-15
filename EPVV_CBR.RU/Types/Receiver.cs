namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация получателей сообщения (сериализуется)
    /// </summary>
    public class Receiver : Sender
    {
        /// <summary>
        /// Инициализация модели получателей сообщения
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика получателя</param>
        /// <param name="ogrn">Основной государственный регистрационный номер получателя</param>
        /// <param name="bik">Банковский идентификационный код получателя</param>
        /// <param name="email">Адрес электронной почты получателя</param>
        /// <param name="regNum">Регистрационный номер КО – получателя по КГРКО</param>
        /// <param name="divisionCode">Номер филиала КО – получателя по КГРКО</param>
        /// <param name="activity">Краткое наименование вида деятельности</param>
        public Receiver(
            string inn,
            string ogrn,
            string bik,
            string email,
            string regNum,
            string divisionCode,
            string activity)
        {
            Inn = inn;
            Ogrn = ogrn;
            Bik = bik;
            Email = email;
            RegNum = regNum;
            DivisionCode = divisionCode;
            Activity = activity;
        }

        /// <summary>
        /// Адрес электронной почты получателя
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Краткое наименование вида деятельности
        /// </summary>
        public string Activity { get; set; } = default!;
    }
}
