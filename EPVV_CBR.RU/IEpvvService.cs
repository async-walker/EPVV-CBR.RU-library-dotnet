using EPVV_CBR.RU.Models;

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
        /// <param name="messageBody">Передаваемое тело сообщения</param>
        /// <returns>Ответ из тела сообщения</returns>
        Task<ResponseMessageBody> CreateDraftMessage(RequestMessageBody messageBody);
        /// <summary>
        /// <para>Создание сессий отправки</para>
        /// <para>POST: /messages{messageId}/files/{fileId}/createUploadSession</para>
        /// </summary>
        /// <param name="responseMessageBody"></param>
        /// <returns></returns>
        Task<List<SessionInfo>> CreateUploadSessions(ResponseMessageBody responseMessageBody);
        /// <summary>
        /// <para>Загрузка файлов на сервер</para>
        /// <para>PUT: /messages/{messageId}/files/{fileId}</para>
        /// </summary>
        /// <param name="sessions"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        Task<List<UploadedFile>> UploadFiles(List<SessionInfo> sessions, string folderPath);
        /// <summary>
        /// <para>Подтверждение отправки сообщения на сервер</para>
        /// <para>POST: /messages/{messageId}</para>
        /// </summary>
        /// <param name="messageId">ID сообщения</param>
        /// <returns></returns>
        Task ConfirmSendMessage(string messageId);
        /// <summary>
        /// <para>Скачивание файлов из репозитория сообщения в пользовательскую директорию</para>
        /// </summary>
        /// <param name="message">Информация о сообщении</param>
        /// <param name="directory">Директория, куда следует сохранить скачиваемые файлы</param>
        /// <returns></returns>
        Task<List<string>> DownloadFilesFromRepository(MessageInfo message, string directory);
        /// <summary>
        /// Скачивание файлов в виде списка массивов байтов
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="file"></param>
        /// <param name="directory"></param>
        /// <returns>Путь к скачанному файлу</returns>
        Task<string> DownloadFileFromRepository(string messageId, MessageFileUploaded file, string directory);
        /// <summary>
        /// <para>Получение HTTP-контента скачиваемого файла</para>
        /// </summary>
        /// <param name="messageId">ID сообщения</param>
        /// <param name="fileId">ID файла</param>
        /// <returns><see cref="HttpContent"/></returns>
        Task<HttpContent> GetFileDataFromRepository(string messageId, string fileId);
        /// <summary>
        /// <para>Получение информации о всех сообщениях с учетом необязательного фильтра (не более 100 сообщений за один запрос)</para>
        /// <para>GET: /messages?[params]</para>
        /// </summary>
        /// <param name="queryParams">Критерии поиска сообщений</param>
        /// <returns></returns>
        Task<List<MessageInfo>> GetMessagesInfoByParameters(QueryParameters? queryParams = null);
        /// <summary>
        /// <para>Получение информации о сообщении по его ID</para>
        /// <para>GET: /messages/{messageId}</para>
        /// </summary>
        /// <param name="messageId">ID сообщения</param>
        /// <returns></returns>
        Task<MessageInfo> GetMessageInfoById(string messageId);
    }
}
