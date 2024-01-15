using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Types.Enums;

namespace EPVV_CBR_RU
{
    /// <summary>
    /// Набор настроек для сервиса взаимодействия с ЕПВВ ЦБ
    /// </summary>
    public class EpvvClientOptions
    {
        const string TestPortalUrl = "https://portal5test.cbr.ru/back/rapi2";
        const string PortalUrl = "https://portal5.cbr.ru/back/rapi2";

        private static bool _isTestPortal;

        /// <summary>
        /// Базовый адрес в зависимости от выбранного типа портала
        /// </summary>
        public string BaseAddress 
        {
            get
            {
                if (_isTestPortal)
                    return TestPortalUrl;
                else return PortalUrl;
            } 
        }
        /// <summary>
        /// Зашифрованные учетные данные (Basic auth)
        /// </summary>
        public string Credentials { get; }
        /// <summary>
        /// <para>Тип репозитория, в который будут загружаться файлы</para>
        /// <para>По умолчанию <b>http</b></para>
        /// </summary>
        public RepositoryType RepositoryType { get; }

        /// <summary>
        /// Инициализация опций для клиента
        /// </summary>
        /// <param name="testPortal">Указать, использовать ли тестовый портал</param>
        /// <param name="username">Имя учетной записи портала portal5.cbr.ru (portal5test.cbr.ru)</param>
        /// <param name="password">Пароль от учетной записи</param>
        /// <param name="repositoryType">Тип загрузки файлов в репозиторий</param>
        public EpvvClientOptions(
            string username, 
            string password, 
            bool testPortal = false, 
            RepositoryType repositoryType = RepositoryType.http)
        {
            _isTestPortal = testPortal;
            Credentials = GetEncodeCredentials(username, password);
            RepositoryType = repositoryType;
        }

        private static string GetEncodeCredentials(string username, string password)
        {
            var template = $"{username}:{password}";

            return template.EncodeToBase64();
        }
    }
}
