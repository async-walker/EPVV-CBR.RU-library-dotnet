﻿namespace EPVV_CBR_RU.Enums
{
    /// <summary>
    /// Тип файла
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// Любые данные, которые не проходят логический контроль непосредственно на ВП ЕПВВ (файлы документов, любые архивы, в т.ч. зашифрованные, неструктурированные данные и другие файлы)
        /// </summary>
        Document,
        /// <summary>
        /// Xml файл определенной структуры, который может быть проверен ВП ЕПВВ на соответствие его схеме
        /// </summary>
        SerializedWebForm,
        /// <summary>
        /// Файл УКЭП, проверка которой влияет на прием/отбраковку сообщения, применяется для основной подписи сообщения и подписи машиночитаемой доверенности
        /// </summary>
        Sign,
        /// <summary>
        /// Файл машиночитаемой доверенности
        /// </summary>
        PowerOfAttorney
    }
}
