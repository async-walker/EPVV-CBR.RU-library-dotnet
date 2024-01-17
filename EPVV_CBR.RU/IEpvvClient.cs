using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Requests.Abstractions;

namespace EPVV_CBR_RU
{
    /// <summary>
    /// Интерфейс взаимодействия с API
    /// </summary>
    public interface IEpvvClient
    {
        /// <summary>
        /// Экземпляр <see cref="IExceptionParser"/> для парсинга ошибок API
        /// </summary>
        IExceptionParser ExceptionsParser { get; set; }

        /// <summary>
        /// Отправка запроса к API без десериализации ответа
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task MakeRequestAsync(
            IRequest request,
            CancellationToken cancellationToken = default);

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
        /// <param name="path"></param>
        /// <param name="destination"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task DownloadFileAsync(
            string path,
            Stream destination,
            CancellationToken cancellationToken = default);
    }
}
