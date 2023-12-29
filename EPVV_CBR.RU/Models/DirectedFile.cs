using EPVV_CBR_RU.Enums;
using Newtonsoft.Json;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Репрезентация файла, включенного в отправляемое сообщение
    /// </summary>
    public class DirectedFile
    {
        /// <summary>
        /// Инициализация файла, включенного в отправляемое сообщение
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <param name="fileType">Тип файла</param>
        /// <param name="encrypted">Признак зашифрованности файла</param>
        /// <param name="size">Размер отправляемого файла в байтах</param>
        /// <param name="description">Описание файла</param>
        /// <param name="signedFile">Имя и расширение другого приложенного файла сообщения,  подписью для которого является данный файл (заполняется только для файлов подписи *.sig)</param>
        /// <param name="repositoryType">Необязательный параметр, указывающий тип репозитория, в который пользователь будет загружать файл. В случае если не установлен, то зависит от характеристик задачи</param>
        /// <param name="repositoryInfo">Информация о характеристиках репозитория, в который будет загружен файл. Заполняется в случае, если указан RepositoryType = aspera</param>
        public DirectedFile(
            string name, 
            FileType fileType, 
            bool encrypted, 
            long size, 
            string? description = default,
            string? signedFile = default,
            RepositoryType? repositoryType = default,
            RepositoryInfo? repositoryInfo = default)
        {
            Name = name;
            FileType = fileType;
            Encrypted = encrypted;
            Size = size;
            Description = description;
            SignedFile = signedFile;
            RepositoryType = repositoryType;
            RepositoryInfo = repositoryInfo;
        }

        /// <summary>
        /// Имя файла
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
        /// <summary>
        /// Описание файла (необязательное поле, для запросов и предписаний из Банка России содержит имя файла с расширением, однако может содержать запрещённые символы Windows)
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Тип файла
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public FileType FileType { get; set; }
        /// <summary>
        /// Признак зашифрованности файла
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool Encrypted { get; set; }
        /// <summary>
        /// Имя и расширение другого приложенного файла сообщения,  подписью для которого является данный файл (заполняется только для файлов подписи *.sig)
        /// </summary>
        public string? SignedFile { get; set; }
        /// <summary>
        /// Размер отправляемого файла в байтах (int64)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public long Size { get; set; }
        /// <summary>
        /// Необязательный параметр, указывающий тип репозитория, в который пользователь будет загружать файл. В случае если не установлен, то зависит от характеристик задачи
        /// </summary>
        public RepositoryType? RepositoryType { get; set; }
        /// <summary>
        /// Информация о характеристиках репозитория, в который будет загружен файл. Заполняется в случае, если указан <b>RepositoryType = aspera</b>
        /// </summary>
        public RepositoryInfo? RepositoryInfo { get; set; }
    }
}
