namespace EPVV_CBR.RU.Extensions
{
    internal static class FileExtensions
    {
        /// <summary>
        /// Запись данных в файл из полученного http-контента
        /// </summary>
        /// <param name="path"></param>
        /// <param name="responseContent"></param>
        /// <returns></returns>
        public static async Task WriteStreamContentToFile(this HttpContent responseContent, string path)
        {
            using (var content = responseContent)
            {
                var data = await content.ReadAsByteArrayAsync();
                using (var fs = new FileStream(path, FileMode.Create))
                    await fs.WriteAsync(data, 0, data.Length);
            }
            responseContent.Dispose();
        }
    }
}
