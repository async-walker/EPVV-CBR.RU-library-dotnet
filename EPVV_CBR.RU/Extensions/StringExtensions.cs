using System.Text;

namespace EPVV_CBR.RU.Extensions
{
    public static class StringExtensions
    {


        //private static string? GetDirectoryTasksFiles()
        //{
        //    var name = EnvironmentVariablesHelper.DirectoryTasksFiles;

        //    var envVariableName = EnvironmentVariablesHelper.GetEnvironmentVariable(name);

        //    return envVariableName ?? throw new EnvironmentVariableNotFoundException();
        //}

        /// <summary>
        /// Название папки по текущей дате в формате ГГГГ-ММ-ДД
        /// </summary>
        /// <returns>Название папки</returns>
        public static string GetFolderNameAtCurrentDate()
        {
            var folderName = DateTime.Now.ToString("yyyy-MM-dd");

            return folderName;
        } 
        /// <summary>
        /// Директория для архивации по типу задачи
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns>Директория для архива</returns>
        //public static string GetArchiveFolderForTask(TaskType taskType)
        //{
        //    var folder = @$"{GetFolderByType(taskType)}\ARCHIVE\{GetFolderNameAtCurrentDate()}";

        //    return folder;
        //}
        /// <summary>
        /// Директория по типу задачи
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns>Путь к директории</returns>
        //public static string GetFolderByType(TaskType taskType)
        //{
        //    var directoryTasksFiles = GetDirectoryTasksFiles();

        //    var folder = @$"{directoryTasksFiles}\{taskType}";

        //    return folder;
        //}
        /// <summary>
        /// Путь к новому файлу с добавлением расширения
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="currentFilePath"></param>
        /// <param name="ext"></param>
        /// <returns>Путь к файлу</returns>
        public static string PathToNewFileWithAddSpecExt(this string directory, string currentFilePath, string ext)
        {
            var path = @$"{directory}\{currentFilePath.FileNameFromPath()}.{ext}";

            return path;
        }
        /// <summary>
        /// Удаление последнего расширение у файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Файл без последнего расширения</returns>
        public static string RemoveLastExt(this string filename)
        {
            var withoutExt = Path.ChangeExtension(filename, null);

            return withoutExt;
        }
        /// <summary>
        /// Объединение пути директории и названия файла
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns>Полный путь</returns>
        public static string CombinePathAndFile(this string path, string filename)
        {
            var fullPath = Path.Combine(path, filename);

            return fullPath;
        }
        /// <summary>
        /// Получение названия файла из пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Название файла</returns>
        public static string FileNameFromPath(this string path)
        {
            var fileName = Path.GetFileName(path);

            return fileName;
        }
        /// <summary>
        /// Приведение первого символа строки в Заглавный
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Строка с первым заглавным символом</returns>
        public static string? UpperFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            return char.ToUpper(input[0]) + input[1..];
        }
        /// <summary>
        /// Шифрование строки
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Зашифрованная строка</returns>
        public static string EncodeToBase64(this string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var encode = Convert.ToBase64String(bytes);

            return encode;
        }
        /// <summary>
        /// Удаляет базовый адрес для конечной точки
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="baseAddress"></param>
        /// <returns>Конечная точка REST-сервиса</returns>
        public static string RemoveBaseAddressForEndpoint(this string uri, Uri baseAddress)
        {
            var endpoint = uri.Replace(baseAddress.ToString(), string.Empty).Remove(0, 1);

            return endpoint;
        }
    }
}
