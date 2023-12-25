using Newtonsoft.Json;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Сессия отправки для загрузки файла на сервер
    /// </summary>
    public class MessageSession
    {
        /// <summary>
        /// Инициализация сессии отправки
        /// </summary>
        /// <param name="messageId">Уникальный идентификатор сообщения в формате UUID [16], полученный в качестве ответа при вызове метода <see cref="IEPVVClient.CreateDraftMessage"/></param>
        /// <param name="fileId">Уникальный идентификатор файла в формате UUID [16], полученный в качестве ответа при вызове метода <see cref="IEPVVClient.CreateDraftMessage"/></param>
        public MessageSession(string messageId, string fileId)
        {
            MessageId = messageId;
            FileId = fileId;
        }

        /// <summary>
        /// Уникальный идентификатор сообщения в формате UUID [16], полученный в качестве ответа при вызове метода <see cref="IEPVVClient.CreateDraftMessage"/>
        /// </summary>
        [JsonProperty("MsgId")]
        public string MessageId { get; set; }
        /// <summary>
        /// Уникальный идентификатор файла в формате UUID [16], полученный в качестве ответа при вызове метода <see cref="IEPVVClient.CreateDraftMessage"/>
        /// </summary>
        [JsonProperty("FileId")]
        public string FileId { get; set; }
    }
}
