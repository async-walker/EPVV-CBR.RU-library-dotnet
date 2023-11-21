using EPVV_CBR.RU.Extensions;

namespace EPVV_CBR.RU
{
    /// <summary>
    /// 
    /// </summary>
    public class EpvvServiceOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string BaseAddress { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Credentials { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="username">Имя учетной записи портала portal5.cbr.ru (portal5test.cbr.ru)</param>
        /// <param name="password">Пароль от учетной записи</param>
        public EpvvServiceOptions(string baseAddress, string username, string password) 
            : this (baseAddress, GetEncodeCredentials(username, password))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="credentials">Зашифрованные аутентификационные данные для формата Base</param>
        public EpvvServiceOptions(string baseAddress, string credentials) 
        {
            BaseAddress = baseAddress;
            Credentials = credentials;
        }

        private static string GetEncodeCredentials(string username, string password)
        {
            var template = $"{username}:{password}";

            return template.EncodeToBase64();
        }
    }
}
