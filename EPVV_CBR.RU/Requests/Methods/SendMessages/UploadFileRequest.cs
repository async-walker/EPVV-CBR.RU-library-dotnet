using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.SendMessages
{
    /// <summary>
    /// Используется для загрузки файла на сервер
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UploadFileRequest : FileRequestBase<UploadedFile>
    {
        /// <summary>
        /// Инициализация загрузки файла на сервер
        /// </summary>
        /// <param name="sessionInfo">Сессия для загрузки файла</param>
        /// <param name="source">Поток с контентом</param>
        public UploadFileRequest(SessionInfo sessionInfo, Stream source)
            : base(HttpMethod.Put, sessionInfo.UploadUrl) =>
            Stream = source;

        private Stream Stream { get; set; }

        /// <inheritdoc />
        public override HttpContent? ToHttpContent() =>
             ToByteArrayContent(Stream);
    }
}
