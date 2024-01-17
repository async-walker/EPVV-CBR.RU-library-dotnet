namespace EPVV_CBR_RU.Types.Enums
{
    /// <summary>
    /// Направление обмена
    /// </summary>
    public enum DirectionExchangeType
    {
        /// <summary>
        /// Входящее (БР->ЛК)
        /// </summary>
        Inbox = 0,
        /// <summary>
        /// Исходящие (ЛК->БР)
        /// </summary>
        Outbox = 1,
        /// <summary>
        /// Двунаправленные (ЛК->ЛК)
        /// </summary>
        Bidirectional = 2
    }
}
