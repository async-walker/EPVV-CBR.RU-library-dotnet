using EPVV_CBR.RU.Enums;

namespace EPVV_CBR.RU.Models
{
    /// <summary>
    /// Репрезентация полученной информации о файле в сообщении
    /// </summary>
    public class ReceivedFile
    {
        /// <summary>
        /// Уникальный идентификатор файла в формате UUID [16]
        /// </summary>
        public string Id { get; set; } = default!;
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Описание файла (необязательное поле, для запросов и предписаний из Банка России содержит имя файла с расширением, однако может содержать запрещённые символы Windows)
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Тип файла
        /// </summary>
        public FileType FileType { get; set; }
        /// <summary>
        /// Признак зашифрованности файла
        /// </summary>
        public bool Encrypted { get; set; }
        /// <summary>
        /// Идентификатор файла сообщения, подписью для которого является данный файл (заполняется только для файлов подписи *.sig)
        /// </summary>
        public string? SignedFile { get; set; }
        /// <summary>
        /// Общий размер файла в байтах
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Информация о репозиториях (описание репозитория в котором расположен файл. Данная информация используется как для загрузки файла, так и при его выгрузке)
        /// </summary>
        public List<ReceivedRepositoryInfo> RepositoryInfo { get; set; } = default!;
    }
}
