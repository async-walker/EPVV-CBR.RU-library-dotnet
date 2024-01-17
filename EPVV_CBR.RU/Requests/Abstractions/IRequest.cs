namespace EPVV_CBR_RU.Requests.Abstractions
{
    /// <summary>
    /// Репрезентация запроса к API
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// HTTP метод, по которому передается запрос
        /// </summary>
        HttpMethod Method { get; }
        /// <summary>
        /// Путь, куда передается запрос
        /// </summary>
        string Path { get; }
        /// <summary>
        /// Метод для генерации <see cref="HttpContent"/>
        /// </summary>
        /// <returns></returns>
        HttpContent? ToHttpContent();
    }
}
