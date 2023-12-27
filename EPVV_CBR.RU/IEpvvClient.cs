using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Requests.Abstractions;

namespace EPVV_CBR_RU
{
    /// <summary>
    /// Интерфейс для взаимодействия с ЕПВВ
    /// </summary>
    public interface IEpvvClient
    {
        /// <summary>
        /// Экземпляр <see cref="IExceptionParser"/> для парсинга ошибок API
        /// </summary>
        IExceptionParser ExceptionsParser { get; set; }

        /// <summary>
        /// Отправка запроса к API
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Тест доступности API
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> TestApiAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для загрузки файлов
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destination"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentException">Путь файла - <c>null</c>, пустой или очень короткий</exception>
        /// <exception cref="ArgumentNullException"><paramref name="destination"/> - <c>null</c></exception>
        Task DownloadFileAsync(
            string filePath,
            Stream destination,
            CancellationToken cancellationToken = default);
    }
}
