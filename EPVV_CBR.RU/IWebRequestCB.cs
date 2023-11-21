namespace EPVV_CBR.RU
{
    public interface IWebRequestCB
    {
        /// <summary>
        /// Создание черновика сообщения
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: /messages
        Task<ResponseViewModel<ResponseMessageBody>> PostMessages(string data);
        /// <summary>
        /// Создание сессий отправки файлов
        /// </summary>
        /// <param name="messageResponse"></param>
        /// <returns></returns>
        // POST: /messages{messageId}/files/{fileId}/createUploadSession
        Task<ResponseViewModel<List<SessionInfo>>> PostMessagesCreateUploadSession(ResponseMessageBody messageResponse);
        /// <summary>
        /// Загрузка файлов на сервер ЦБ
        /// </summary>
        /// <param name="sessionsFiles"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        // PUT: /messages/{messageId}/files/{fileId}
        Task<ResponseViewModel<List<UploadedFile>>> PutMessages(List<SessionInfo> sessions, string folderPath);
        /// <summary>
        /// Подтверждение отправки сообщения на сервер ЦБ
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        // POST: /messages/{messageId}
        Task<ResponseViewModel<object>> PostConfirmSendMessages(string messageId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        Task<ResponseViewModel<List<string>>> GetDownloadFilesFromMessage(MessageInfo messageInfo, string directory);
        /// <summary>
        /// Получение информации о сообщениях по коду задачи
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<ResponseViewModel<List<MessageInfo>>> GetMessagesInfoByParameters(string[] parametersQuery);
        /// <summary>
        /// Получение информации о сообщении по его ID
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        // GET: /messages{messageId}
        Task<ResponseViewModel<MessageInfo>> GetMessageInfoById(string messageId);
        /// <summary>
        /// Уничтожение клиента вызова запросов
        /// </summary>
        void DisposeClient();
    }
}
