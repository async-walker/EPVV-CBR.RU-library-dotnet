using EPVV_CBR.RU.Extensions;

namespace EPVV_CBR.RU
{
    /// <summary>
    /// 
    /// </summary>
    public class WebRequestOptions
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
        /// <param name="username">Имя пользователя</param>
        /// <param name="password"></param>
        public WebRequestOptions(string baseAddress, string username, string password)
        {
            BaseAddress = baseAddress;
            Credentials = GetEncodeCredentials(username, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="credentials">Зашифрованные аутентификационные данные для формата Base</param>
        public WebRequestOptions(string baseAddress, string credentials) 
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
