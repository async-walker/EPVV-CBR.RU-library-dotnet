﻿namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Репрезентация информации о характеристиках репозитория, в который будет загружен файл
    /// </summary>
    public class RepositoryInfo
    {
        /// <summary>
        /// Инициализация модели информации о характеристиках репозитория, в который будет загружен файл
        /// </summary>
        /// <param name="checkSum">Контрольная сумма файла, необходимая для контроля его целостности. Берется пользователем из «манифеста», формируемого ТПС «Aspera» после загрузки файла</param>
        /// <param name="checkSumType">Алгоритм расчёта контрольной суммы файла, в зависимости от установок ТПС «Aspera». Берется пользователем из «манифеста», формируемого ТПС «Aspera» после загрузки файла</param>
        /// <param name="path">Путь к файлу относительно хранилища пользователя в ТПС «Аспера», включая имя файла. Имена файлов должны быть в виде GUID без расширения. Имя генерирует сам пользователь. Берется пользователем из «манифеста», формируемого ТПС «Aspera» после загрузки файла</param>
        public RepositoryInfo(
            string checkSum,
            string checkSumType,
            string path)
        {
            CheckSum = checkSum;
            CheckSumType = checkSumType;
            Path = path;
        }

        public RepositoryInfo() { }

        /// <summary>
        /// Контрольная сумма файла, необходимая для контроля его целостности. Берется пользователем из «манифеста», формируемого ТПС «Aspera» после загрузки файла
        /// </summary>
        public string CheckSum { get; set; }
        /// <summary>
        /// Алгоритм расчёта контрольной суммы файла, в зависимости от установок ТПС «Aspera». Берется пользователем из «манифеста», формируемого ТПС «Aspera» после загрузки файла
        /// </summary>
        public string CheckSumType { get; set; }
        /// <summary>
        /// Путь к файлу относительно хранилища пользователя в ТПС «Аспера», включая имя файла. Имена файлов должны быть в виде GUID без расширения. Имя генерирует сам пользователь. Берется пользователем из «манифеста», формируемого ТПС «Aspera» после загрузки файла
        /// </summary>
        public string Path { get; set; }
    }
}
