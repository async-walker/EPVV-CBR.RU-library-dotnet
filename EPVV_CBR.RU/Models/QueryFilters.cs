﻿using EPVV_CBR_RU.Enums;
using System.Collections.Specialized;
using System.Web;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Онлайн-запрос критериев поиска сообщений
    /// </summary>
    public class QueryFilters
    {
        private const string maskDateTime = "yyyy-MM-dd'T'HH:mm:ss'Z'";

        private readonly DateTime? _minDateTime;
        private readonly DateTime? _maxDateTime;

        /// <summary>
        /// Инициализация онлайн-запроса критериев
        /// </summary>
        /// <param name="task">Наименование задачи (если параметр будет указан, то будут возвращены только сообщения, полученные/отправленные в рамках указанной задачи)</param>
        /// <param name="minDateTime">Минимально возможная дата создания сообщения (если параметр будет указан, то будут возвращены только сообщения, полученные/отправленные с указанной даты, включая ее)</param>
        /// <param name="maxDateTime">Максимально возможная дата создания сообщения (если параметр будет указан, то будут возвращены только сообщения, полученные/отправленные ранее указанной даты, включая ее)</param>
        /// <param name="minSize">Минимально возможный размер сообщения в байтах (если параметр будет указан, то будут возвращены только сообщения больше и равно указанного размера)</param>
        /// <param name="maxSize">Максимально возможный размер сообщения в байтах (если параметр будет указан, то будут возвращены только сообщения меньше и равно указанного размера)</param>
        /// <param name="type">Тип сообщения (если параметр будет указан, то будут возвращены только сообщения соответствующего типа)</param>
        /// <param name="status">Статус сообщения (если параметр будет указан, то будут возвращены только сообщения с соответствующим статусом)</param>
        /// <param name="page"><para>Номер страницы списка сообщений в разбивке по 100 сообщений (если не задан, то вернутся первые 100 сообщений)</para>
        /// <para>Пример: GET: */messages?page={n}, где {n} – номер страницы содержащей 100 сообщений (n-я сотня сообщений). </para>
        /// <para>Допустимые значения: n > 0 (положительные целые числа, больше 0). </para>
        /// <para>Если запрос страницы не указан, возвращается первая страница сообщений. Если n за границами диапазона страниц, то вернется пустой массив сообщений. </para>
        /// <para>В случае некорректного номера страницы – ошибка</para></param>
        public QueryFilters(
            string? task = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null,
            int? minSize = null,
            int? maxSize = null,
            MessageType? type = null,
            MessageStatus? status = null,
            int? page = null)
        {
            Task = task;
            MinSize = minSize;
            MaxSize = maxSize;
            Type = type;
            Status = status;
            Page = page;

            _minDateTime = minDateTime;
            _maxDateTime = maxDateTime;
        }

        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string? Task { get; }
        /// <summary>
        /// Минимально возможная дата создания сообщения (ГОСТ ISO 8601-2001 по маске «yyyy-MM-dd’T’HH:mm:ss’Z’») 
        /// </summary>
        public string? MinDateTime 
        {
            get
            {
                if (_minDateTime is not null)
                    return _minDateTime.Value.ToString(maskDateTime);
                else return null;
            }
        }
        /// <summary>
        /// Максимально возможная дата создания сообщения (ГОСТ ISO 8601-2001 по маске «yyyy-MM-dd’T’HH:mm:ss’Z’») 
        /// </summary>
        public string? MaxDateTime 
        {
            get
            {
                if (_maxDateTime is not null)
                    return _maxDateTime.Value.ToString(maskDateTime);
                else return null;
            }
        }
        /// <summary>
        /// Минимально возможный размер сообщения в байтах 
        /// </summary>
        public int? MinSize { get; }
        /// <summary>
        /// Максимально возможный размер сообщения в байтах
        /// </summary>
        public int? MaxSize { get; }
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public MessageType? Type { get; }
        /// <summary>
        /// Статус сообщения
        /// </summary>
        public MessageStatus? Status { get; }
        /// <summary>
        /// Номер страницы списка сообщений в разбивке по 100 сообщений
        /// </summary>
        public int? Page { get; }

        internal static string ExecuteParams(QueryFilters? queryFilters)
        {
            if (queryFilters is null)
                return string.Empty;

            string query = string.Empty;
            NameValueCollection queryCollection = [];

            if (queryFilters.Task is not null)
                queryCollection.Add(nameof(Task), queryFilters.Task);
            if (queryFilters.MinDateTime is not null)
                queryCollection.Add(nameof(MinDateTime), queryFilters.MinDateTime);
            if (queryFilters.MaxDateTime is not null)
                queryCollection.Add(nameof(MaxDateTime), queryFilters.MaxDateTime);
            if (queryFilters.MinSize is not null)
                queryCollection.Add(nameof(MinSize), queryFilters.MinSize.ToString());
            if (queryFilters.MaxSize is not null)
                queryCollection.Add(nameof(MaxSize), queryFilters.MaxSize.ToString());
            if (queryFilters.Type is not null)
                queryCollection.Add(nameof(Type), queryFilters.Type.ToString());
            if (queryFilters.Status is not null)
                queryCollection.Add(nameof(Status), queryFilters.Status.ToString());
            if (queryFilters.Page is not null)
                queryCollection.Add(nameof(Page), queryFilters.Page.ToString());

            if (queryCollection.Count > 0)
            {
                var queryParamsArray = (
                    from key in queryCollection.AllKeys
                    from value in queryCollection.GetValues(key)!
                    select string.Format(
                        "{0}={1}",
                        HttpUtility.UrlEncode(key),
                        HttpUtility.UrlEncode(value))
                ).ToArray();

                query += "?" + string.Join("&", queryParamsArray);
            }

            return query;
        }
    }
}
