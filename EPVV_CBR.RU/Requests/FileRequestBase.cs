using EPVV_CBR_RU.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests
{
    /// <summary>
    /// Репрезентация запроса к API с загрузкой файла
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого результата ответа</typeparam>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class FileRequestBase<TResponse> : RequestBase<TResponse>
    {
        /// <summary>
        /// Инициализация экземпляра запроса
        /// </summary>
        /// <param name="method">HTTP метод для использования</param>
        /// <param name="path">Путь API</param>
        protected FileRequestBase(HttpMethod method, string path)
            : base(method, path)
        { }

        /// <summary>
        /// Генерация контента массива байтов
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected ByteArrayContent ToByteArrayContent(Stream stream)
        {
            if (stream is null or { Length: 0 })
                throw new ArgumentNullException("Поток с контентом пустой");

            var bytes = stream.ConvertToByteArray();

            return GenerateByteArrayContent(bytes);
        }

        /// <summary>
        /// Генерация контента массива байтов
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected ByteArrayContent GenerateByteArrayContent(byte[] bytes)
        {
            var byteArrayContent = new ByteArrayContent(bytes)
            {
                Headers =
                {
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Length", $"{bytes.Length}" },
                    {"Content-Range", $"bytes 0-{bytes.Length-1}/{bytes.Length}" }
                },
            };

            return byteArrayContent;
        }
    }
}
