namespace EPVV_CBR.RU
{
    /// <summary>
    /// Интерфейс для взаимодействия с ЕПВВ
    /// </summary>
    public interface IEpvvService : IDisposable
    {
        /// <summary>
        /// <para>Создание черновика сообщения</para>
        /// <para>POST: /messages</para>
        /// </summary>
        /// <param name="data">Передаваемый json-контент</param>
        /// <returns></returns>
        Task<ResponseViewModel<ResponseMessageBody>> PostMessages(string data);
        /// <summary>
        /// <para>Создание сессий отправки файлов</para>
        /// <para>POST: /messages{messageId}/files/{fileId}/createUploadSession</para>
        /// </summary>
        /// <param name="responseMessageBody"></param>
        /// <returns></returns>
        Task<ResponseViewModel<List<SessionInfo>>> PostMessagesCreateUploadSession(ResponseMessageBody responseMessageBody);
        /// <summary>
        /// <para>Загрузка файлов на сервер ЦБ</para>
        /// <para>PUT: /messages/{messageId}/files/{fileId}</para>
        /// </summary>
        /// <param name="sessions"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        Task<ResponseViewModel<List<UploadedFile>>> PutMessages(List<SessionInfo> sessions, string folderPath);
        /// <summary>
        /// <para>Подтверждение отправки сообщения на сервер ЦБ</para>
        /// <para>POST: /messages/{messageId}</para>
        /// </summary>
        /// <param name="messageId">ID сообщения</param>
        /// <returns></returns>
        Task<ResponseViewModel<object>> PostConfirmSendMessages(string messageId);
        /// <summary>
        /// <para>Скачивание файлов из репозитория сообщения</para>
        /// <para>GET: /messages/{messageId}/files/{fileId}/download</para>
        /// </summary>
        /// <param name="messageInfo">Информация о сообщении</param>
        /// <param name="directory">Директория, куда следует сохранить скачиваемые файлы</param>
        /// <returns></returns>
        Task<ResponseViewModel<List<string>>> GetDownloadFilesFromMessage(MessageInfo messageInfo, string directory);
        /// <summary>
        /// <para>Получение информации о сообщениях по параметрам</para>
        /// <para>GET: /messages?[params]</para>
        /// </summary>
        /// <param name="parametersQuery">Критерии поиска сообщений</param>
        /// <returns></returns>
        Task<ResponseViewModel<List<MessageInfo>>> GetMessagesInfoByParameters(string[] parametersQuery);
        /// <summary>
        /// <para>Получение информации о сообщении по его ID</para>
        /// <para>GET: /messages/{messageId}</para>
        /// </summary>
        /// <param name="messageId">ID сообщения</param>
        /// <returns></returns>
        Task<ResponseViewModel<MessageInfo>> GetMessageInfoById(string messageId);
    }
}
