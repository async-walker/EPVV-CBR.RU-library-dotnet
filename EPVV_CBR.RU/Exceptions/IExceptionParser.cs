namespace EPVV_CBR_RU.Exceptions;

/// <summary>
/// Парсинг исключений неудачных ответов от API
/// </summary>
public interface IExceptionParser
{
    /// <summary>
    /// Парсинг HTTP ответа с исключением
    /// </summary>
    /// <param name="error">Ошибка API с ответом</param>
    /// <returns></returns>
    ApiRequestException Parse(ApiResponseError error);
}
