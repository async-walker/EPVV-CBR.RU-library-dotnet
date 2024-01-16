namespace EPVV_CBR_RU.Requests
{
    /// <summary>
    /// Репрезентация запроса к API
    /// </summary>
    public interface IRequest<TResponse>
    {
        /// <summary>
        /// HTTP метод, по которому передается запрос
        /// </summary>
        HttpMethod Method { get; }
        /// <summary>
        /// Конечная точка, на которую передается запрос
        /// </summary>
        string Endpoint { get; }
        /// <summary>
        /// Метод для генерации <see cref="HttpContent"/>
        /// </summary>
        /// <returns></returns>
        HttpContent? ToHttpContent();
    }
}
