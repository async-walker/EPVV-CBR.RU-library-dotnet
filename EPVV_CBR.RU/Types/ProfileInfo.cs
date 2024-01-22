namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о профиле участника информационного обмена (десериализуется)
    /// </summary>
    public class ProfileInfo
    {
        /// <summary>
        /// Краткое наименование компании
        /// </summary>
        public string? ShortName { get; set; }
        /// <summary>
        /// Полное наименование компании
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Список видов деятельностей компании
        /// </summary>
        public List<Activity>? Activities { get; set; }
        /// <summary>
        /// ИНН компании 
        /// </summary>
        public string? Inn { get; set; }
        /// <summary>
        /// ОГРН компании 
        /// </summary>
        public string? Ogrn { get; set; }
        /// <summary>
        /// Международный идентификатор
        /// </summary>
        public string? InternationalId { get; set; }
        /// <summary>
        /// Организационно-правовая форма компании 
        /// </summary>
        public string? Opf { get; set; }
        /// <summary>
        /// Электронный адрес компании 
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Почтовый адрес компании 
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Контактный телефон компании 
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Дата создания ЛК компании
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Текущий статус ЛК компании
        /// </summary>
        public string? Status { get; set; }
    }

    /// <summary>
    /// Репрезентация вида деятельности компании (десериализуется)
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Полное наименование вида деятельности
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Краткое наименование вида деятельности
        /// </summary>
        public string? ShortName { get; set; }
        /// <summary>
        /// Поднадзорное подразделение
        /// </summary>
        public SupervisionDevision? SupervisionDevision { get; set; }
    }

    /// <summary>
    /// Репрезентация поднадзорного подразделения (десериализуется)
    /// </summary>
    public class SupervisionDevision
    {
        /// <summary>
        /// Наименование поднадзорного подразделения Банка России 
        /// </summary>
        public string? Name { get; set; }
    }
}
