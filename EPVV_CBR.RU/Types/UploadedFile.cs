using EPVV_CBR_RU.Types.Enums;
using EPVV_CBR_RU.Types.Responses;

namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Информация о загружаемом на сервер файла
    /// </summary>
    public class UploadedFile
    {
        /// <summary>
        /// Уникальный идентификатор сообщения в формате
        /// </summary>
        public string Id { get; set; } = default!;
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Тип файла
        /// </summary>
        public FileType? FileType { get; set; }
        /// <summary>
        /// Описание файла (необязательно поле, для запросов и предписаний из Банка России содержит имя файла с расширением, однако может содержать запрещённые символы Windows)
        /// </summary>
        public string? Description { get; set; }
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
        public List<AdvancedRepositoryInfo> RepositoryInfo { get; set; } = default!;
    }
}
