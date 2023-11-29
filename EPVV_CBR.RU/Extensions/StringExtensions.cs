using System.Text;

namespace EPVV_CBR.RU.Extensions
{
    internal static class StringExtensions
    {
        public static string EncodeToBase64(this string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var encode = Convert.ToBase64String(bytes);

            return encode;
        }

        public static string RemoveBaseAddressForEndpoint(this string uri, Uri baseAddress)
        {
            var endpoint = uri.Replace(baseAddress.ToString(), string.Empty).Remove(0, 1);

            return endpoint;
        }
    }
}
