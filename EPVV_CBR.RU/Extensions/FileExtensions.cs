using System.IO.Compression;
using System.Text;

namespace EPVV_CBR.RU.Extensions
{
    public static class FileExtensions
    {
        ///// <summary>
        ///// Получение XML-файла из директории
        ///// </summary>
        ///// <param name="folderPath"></param>
        ///// <returns>Путь к XML-файлу</returns>
        ///// <exception cref="Exception"></exception>
        //public static async Task<string> GetXmlFile(this string folderPath, bool onetime = true, bool garant = true)
        //{
        //    var path = Directory.GetFiles(folderPath, "*.xml");

        //    if (!onetime)
        //    {
        //        bool xml = false;

        //        for (int i = 0; i < 15; i++)
        //        {
        //            try
        //            {
        //                xml = XmlIsExist(path);
                        
        //                if (xml)
        //                    break;
        //            }
        //            catch
        //            {
        //                await Task.Delay(TimeSpan.FromMinutes(5));

        //                path = Directory.GetFiles(folderPath, "*.xml");
        //            }
        //        }

        //        if (!xml)
        //            throw new Exception("Не удалось найти файл с клиентской базой (15 попыток)");
        //    }
        //    else 
        //    {
        //        if (!garant)
        //        {
        //            if (path.Length == 0)
        //                return string.Empty;

        //            return path[0];
        //        }

        //        XmlIsExist(path); 
        //    }

        //    return path[0];
        //}

        //private static bool XmlIsExist(string[] path)
        //{
        //    if (path.Length == 0) { throw new Exception("В папке исходящих отсутствует XML-файл с клиентской базой"); }
        //    else if (path.Length > 1) { throw new Exception("В папке исходящих больше одного XML-файла"); }
        //    else return true;
        //}
        ///// <summary>
        ///// Перемещение файлов в архив
        ///// </summary>
        ///// <param name="files"></param>
        ///// <param name="destPath"></param>
        //public static void MoveFiles(this List<string> files, string destPath)
        //{
        //    if (!Directory.Exists(destPath))
        //        Directory.CreateDirectory(destPath);

        //    foreach (var file in files)
        //    {
        //        var destFileName = $@"{destPath}\{file.FileNameFromPath()}";

        //        File.Move(file, destFileName, true);
        //    }
        //}
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
        ///// <summary>
        ///// Распаковка архива в рамках задачи '130' (Получение информации об уровне риска ЮЛ / ИП)
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns>Путь к распакованному файлу</returns>
        //public static string UnZipArchiveZadacha130(this string path)
        //{
        //    var sourceArchiveFileName = path;
        //    var destinationDirectoryName = StringExtensions.GetFolderByType(TaskType.IN);

        //    ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, true);

        //    var extractFilePath = destinationDirectoryName.CombinePathAndFile(sourceArchiveFileName.FileNameFromPath());

        //    return extractFilePath.RemoveLastExt();
        //}
        ///// <summary>
        ///// Упаковка файлов в архив
        ///// </summary>
        ///// <param name="sourceFilePath"></param>
        ///// <returns>Путь к сформированному архиву</returns>
        //public static string CreateZipArchive(this string sourceFilePath)
        //{
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //    var archiveFileName = sourceFilePath.RemoveLastExt() + ".zip";

        //    using (FileStream zipFile = File.Open(archiveFileName, FileMode.Create))
        //    {
        //        using (FileStream source = File.Open(sourceFilePath, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var archive = new Archive(new ArchiveEntrySettings()))
        //            {
        //                archive.CreateEntry(sourceFilePath.FileNameFromPath(), source);
        //                archive.Save(zipFile);
        //            }
        //        }
        //    }

        //    return archiveFileName;
        //}
        ///// <summary>
        ///// Получение архивов с особым расширением
        ///// </summary>
        ///// <param name="folderPath"></param>
        ///// <param name="extension"></param>
        ///// <returns>Список файлов с указанным расширением</returns>
        //static List<string> GetZipFilesWithSpecExtension(this string folderPath, FileExtension extension)
        //{
        //    var filesAccept = Directory.GetFiles(folderPath, $"*.zip.{extension}").ToList();

        //    return filesAccept;
        //}
        ///// <summary>
        ///// Получение пар зашифрованных файлов и соответсвующих им файлов подписей
        ///// </summary>
        ///// <param name="folderPath"></param>
        ///// <returns>Словарь с парами файлов</returns>
        ///// <exception cref="Exception"></exception>
        //public static Dictionary<string, string> GetEncSigFilesPairs(this string folderPath)
        //{
        //    var pairs = new Dictionary<string, string>();

        //    var encFiles = folderPath.GetZipFilesWithSpecExtension(FileExtension.enc);

        //    foreach (var encFile in encFiles)
        //    {
        //        var sigFile = encFile.Replace(".enc", ".sig");
        //        // если файл подписи существует, добавляем всю пару в словарь
        //        if (File.Exists(sigFile))
        //            pairs.Add(encFile, sigFile);
        //        else
        //            throw new Exception("Для данного зашифованного архива нет файла подписи..");
        //    }

        //    if (pairs.Count == 0)
        //        throw new Exception("Отсутсвуют пары файлов '*.enc' и '*.sig'");

        //    return pairs;
        //}
        ///// <summary>
        ///// Чтение данных файла
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //public static string ReadDataFile(string path, bool json = false)
        //{
        //    using (var sr = new StreamReader(path))
        //    {
        //        var data = sr.ReadToEnd();

        //        if (json)
        //            return data.Replace(Environment.NewLine, "");

        //        return data;
        //    }
        //}
    }
}
