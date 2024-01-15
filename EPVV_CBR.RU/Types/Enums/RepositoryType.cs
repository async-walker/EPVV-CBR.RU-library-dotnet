namespace EPVV_CBR_RU.Types.Enums
{
    /// <summary>
    /// Тип репозитория
    /// </summary>
    public enum RepositoryType
    {
        /// <summary>
        /// Отправка файлов через HTTP
        /// </summary>
        http,
        /// <summary>
        /// Отправка файлов сообщения через FASP (ТПС ASPERA)
        /// </summary>
        aspera,
        /// <summary>
        /// NOT DOCUMENTED IN OFFICIAL DOCUMENTATION REST API CBR.RU
        /// </summary>
        filestorage // Этого нет в оф.документации ЦБ.
                    // Но при получении ответа с конца декабря на тестовом сервере -
                    // данный тип присутсвует в теле сообщения.
    }
}
