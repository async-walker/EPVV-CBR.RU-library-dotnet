using EPVV_CBR.RU.Models;
using System.Collections.Specialized;

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
        /// <returns></returns>
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
        /// <para>Скачивание файлов из репозитория сообщения</para>
        /// <para>GET: /messages/{messageId}/files/{fileId}/download</para>
        /// </summary>
        /// <param name="messageInfo">Информация о сообщении</param>
        /// <param name="directory">Директория, куда следует сохранить скачиваемые файлы</param>
        /// <returns></returns>
        Task<List<string>> DownloadFilesFromRepository(MessageInfo messageInfo, string directory);
        /// <summary>
        /// <para>Получение информации о всех сообщениях с учетом необязательного фильтра (не более 100 сообщений за один запрос)</para>
        /// <para>GET: /messages?[params]</para>
        /// </summary>
        /// <param name="queryParams">Критерии поиска сообщений</param>
        /// <returns></returns>
        Task<List<MessageInfo>> GetMessagesInfoByParameters(NameValueCollection? queryParams = null);
        /// <summary>
        /// <para>Получение информации о сообщении по его ID</para>
        /// <para>GET: /messages/{messageId}</para>
        /// </summary>
        /// <param name="messageId">ID сообщения</param>
        /// <returns></returns>
        Task<MessageInfo> GetMessageInfoById(string messageId);
    }
}
