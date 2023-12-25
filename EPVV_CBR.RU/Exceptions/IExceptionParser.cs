namespace EPVV_CBR_RU.Exceptions;

/// <summary>
/// Парсинг неудачных ответов от API ЕПВВ
/// </summary>
public interface IExceptionParser
{
    /// <summary>
    /// Парасинг HTTP ответа на наличие исключений
    /// </summary>
    /// <param name="apiResponse">Ошибка API с ответом</param>
    /// <returns></returns>
    EPVVException Parse(PortalError apiResponse);
}