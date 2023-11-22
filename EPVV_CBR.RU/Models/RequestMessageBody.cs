namespace EPVV_CBR.RU.Models
{
    /// <summary>
    /// Репрезентация тела сообщения для последующей его сериализации в JSON и передачи через REST-сервис
    /// </summary>
    public class RequestMessageBody
    {
        /// <summary>
        /// Инициализация тела сообщения
        /// </summary>
        /// <param name="task">Код задачи (по справочнику задач в формате "Zadacha_*", где Zadacha_ - неизменная часть, * - число/набор символов определяющий порядковый номер/обозначение задачи), используется для идентификации задачи</param>
        /// <param name="title">Название сообщения, отображается в интерфейсе</param>
        /// <param name="text">Текст сообщения, отображается в интерфейсе</param>
        /// <param name="files">Файлы включенные в сообщение</param>
        /// <param name="correlationId">Идентификатор корреляции сообщения в формате UUID [16] (необязательно, указывается для формирования ответного сообщения для потоков, поддерживаемых данную функциональность)</param>
        /// <param name="groupId">Идентификатор группы сообщений в формате UUID [16] (необязательно, указывается для передачи информации о том, что сообщение является частью группы сообщений для потоков, поддерживаемых данную функциональность)</param>
        /// <param name="receivers">Получатели сообщения (необязательно, указывается для потоков адресной рассылки)</param>
        public RequestMessageBody(
            string task, 
            string title, 
            string text, 
            List<DirectedFile> files,
            string? correlationId = null,
            string? groupId = null,
            List<Receiver>? receivers = null)
        {
            Task = task;
            CorrelationId = correlationId;
            GroupId = groupId;
            Title = title;
            Text = text;
            Files = files;
            Receivers = receivers;
        }

        /// <summary>
        /// Код задачи (по справочнику задач в формате "Zadacha_*", где Zadacha_ - неизменная часть, * - число/набор символов определяющий порядковый номер/обозначение задачи), используется для идентификации задачи
        /// </summary>
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
        public string Title { get; set; }
        /// <summary>
        /// Текст сообщения, отображается в интерфейсе
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Файлы включенные в сообщение
        /// </summary>
        public List<DirectedFile> Files { get; set; }
        /// <summary>
        /// Получатели сообщения (необязательно, указывается для потоков адресной рассылки)
        /// </summary>
        public List<Receiver>? Receivers { get; set; }
    }
}
