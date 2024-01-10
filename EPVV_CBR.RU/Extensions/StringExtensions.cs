using System.Runtime.CompilerServices;
using System.Text;

namespace EPVV_CBR_RU.Extensions
{
    internal static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static string EncodeUtf8(this string value) =>
        new(Encoding.UTF8.GetBytes(value).Select(Convert.ToChar).ToArray());

        internal static string EncodeToBase64(this string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var encode = Convert.ToBase64String(bytes);

            return encode;
        }

        internal static string TryRemoveBaseAddressForEndpoint(this string uri, string baseAddress)
        {
            var endpoint = uri;

            if (uri.Contains(baseAddress))
                endpoint = uri.Replace(baseAddress, string.Empty).Remove(0, 1);

            return endpoint;
        }
    }
}
