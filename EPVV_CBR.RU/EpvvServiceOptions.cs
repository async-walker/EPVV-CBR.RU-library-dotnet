using EPVV_CBR.RU.Data.Enums;
using EPVV_CBR.RU.Extensions;
using Newtonsoft.Json;

namespace EPVV_CBR.RU
{
    /// <summary>
    /// 
    /// </summary>
    public class EpvvServiceOptions
    {
        private static readonly string _testPortalUrl;
        private static readonly string _portalUrl;

        private static bool _isTestPortal;

        static EpvvServiceOptions()
        {
            var pathSettings = "appsettings.json";

            using (var sr = new StreamReader(pathSettings))
            {
                var data = sr.ReadToEnd();

                var appSettings = JsonConvert.DeserializeObject<AppSettings>(data);

                _testPortalUrl = appSettings.TestPortalUrl;
                _portalUrl = appSettings.PortalUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BaseAddress 
        {
            get
            {
                if (_isTestPortal)
                    return _testPortalUrl;
                else return _portalUrl;
            } 
        }
        /// <summary>
        /// 
        /// </summary>
        public string Credentials { get; }
        /// <summary>
        /// 
        /// </summary>
        public RepositoryType RepositoryType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPortal"></param>
        /// <param name="username">Имя учетной записи портала portal5.cbr.ru (portal5test.cbr.ru)</param>
        /// <param name="password">Пароль от учетной записи</param>
        /// <param name="repositoryType">Тип загрузки файлов в репозиторий ЦБ</param>
        public EpvvServiceOptions(bool testPortal, string username, string password, RepositoryType repositoryType = RepositoryType.http)
            : this(testPortal, GetEncodeCredentials(username, password), repositoryType)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPortal"></param>
        /// <param name="credentials">Зашифрованные аутентификационные данные для формата Base</param>
        /// <param name="repositoryType"></param>
        public EpvvServiceOptions(bool testPortal, string credentials, RepositoryType repositoryType)
        {
            _isTestPortal = testPortal;
            Credentials = credentials;
            RepositoryType = repositoryType;
        }

        private static string GetEncodeCredentials(string username, string password)
        {
            var template = $"{username}:{password}";

            return template.EncodeToBase64();
        }

        private struct AppSettings
        {
            public string TestPortalUrl { get; set; }
            public string PortalUrl { get; set;}
        }
    }
}
