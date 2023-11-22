using Newtonsoft.Json;

namespace EPVV_CBR.RU.Extensions
{
    internal static class JsonExtensions
    {
        public static string SerializeToJson(this object body)
        {
            var jsonData = JsonConvert.SerializeObject(body);

            return jsonData;
        }

        public static Model DeserializeFromJson<Model>(this string data)
        {
            var deserialize = JsonConvert.DeserializeObject<Model>(data);

            return deserialize;
        }
    }
}
