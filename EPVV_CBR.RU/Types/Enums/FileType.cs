using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPVV_CBR_RU.Types.Enums
{
    /// <summary>
    /// Тип файла
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FileType
    {
        /// <summary>
        /// Любые данные, которые не проходят логический контроль непосредственно на ВП ЕПВВ (файлы документов, любые архивы, в т.ч. зашифрованные, неструктурированные данные и другие файлы)
        /// </summary>
        Document,
        /// <summary>
        /// Xml файл определенной структуры, который может быть проверен ВП ЕПВВ на соответствие его схеме
        /// </summary>
        SerializedWebForm,
        /// <summary>
        /// Файл УКЭП, проверка которой влияет на прием/отбраковку сообщения, применяется для основной подписи сообщения и подписи машиночитаемой доверенности
        /// </summary>
        Sign,
        /// <summary>
        /// Файл машиночитаемой доверенности
        /// </summary>
        PowerOfAttorney,
        Comment,
        Passport
    }
}
