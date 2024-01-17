using EPVV_CBR_RU.Requests.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests
{
    /// <summary>
    /// Репрезентация запроса к API
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого результата ответа</typeparam>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class RequestBase<TResponse> : Request, IRequest<TResponse>
    {
        /// <inheritdoc/>
        protected RequestBase(HttpMethod method, string path) 
            : base(method, path)
        { }
    }
}
