using EPVV_CBR_RU.Requests.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace EPVV_CBR_RU.Requests
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class RequestBase<TResponse> : IRequest<TResponse>
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public HttpMethod Method { get; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string Endpoint { get; }

        protected RequestBase(
            HttpMethod method, 
            string endpoint)
        {
            Method = method;
            Endpoint = endpoint;
        }

        /// <summary>
        /// Генерация HTTP-контента
        /// </summary>
        /// <returns>HTTP-контент запроса</returns>
        public virtual HttpContent? ToHttpContent() =>
            new StringContent(
                content: JsonConvert.SerializeObject(this),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
    }
}
