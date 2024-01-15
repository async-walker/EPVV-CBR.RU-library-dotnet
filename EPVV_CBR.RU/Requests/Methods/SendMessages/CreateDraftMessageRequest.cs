using EPVV_CBR_RU.Types;
using EPVV_CBR_RU.Types.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.SendMessages
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CreateDraftMessageRequest : RequestBase<DraftMessage>
    {
        /// <summary>
        /// Код задачи (по справочнику задач в формате "Zadacha_*", где Zadacha_ - неизменная часть, * - число/набор символов определяющий порядковый номер/обозначение задачи), используется для идентификации задачи
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Task { get; set; }
        /// <summary>
        /// Идентификатор корреляции сообщения в формате UUID [16] (необязательно, указывается для формирования ответного сообщения для потоков, поддерживаемых данную функциональность)
        /// </summary>
        public string? CorrelationId { get; set; }
        /// <summary>
        /// Идентификатор группы сообщений в формате UUID [16] (необязательно, указывается для передачи информации о том, что сообщение является частью группы сообщений для потоков, поддерживаемых данную функциональность)
        /// </summary>
        public string? GroupId { get; set; }
        /// <summary>
        /// Название сообщения, отображается в интерфейсе
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }
        /// <summary>
        /// Текст сообщения, отображается в интерфейсе
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }
        /// <summary>
        /// Файлы включенные в сообщение
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public List<DirectedFile> Files { get; set; }
        /// <summary>
        /// Получатели сообщения (необязательно, указывается для потоков адресной рассылки)
        /// </summary>
        public List<Receiver>? Receivers { get; set; }

        public CreateDraftMessageRequest(
            string task,
            string title,
            string text,
            List<DirectedFile> files,
            string? correlationId = default,
            string? groupId = default,
            List<Receiver>? receivers = default)
            : base(HttpMethod.Post, "messages")
        {
            Task = task;
            Title = title;
            Text = text;
            Files = files;
            CorrelationId = correlationId;
            GroupId = groupId;
            Receivers = receivers;
        }
    }
}
