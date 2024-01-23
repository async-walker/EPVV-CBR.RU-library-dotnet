namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о пагинации (десериализуется)
    /// </summary>
    public class PaginationInfo
    {
        /// <summary>
        /// Общее число записей
        /// </summary>
        public int? TotalRecords { get; set; }
        /// <summary>
        /// Общее число страниц с разбивкой не более 100 записей на странице
        /// </summary>
        public int? TotalPages { get; set; }
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int? CurrentPage { get; set; }
        /// <summary>
        /// Количество записей на текущей странице. Null, если запрошенная страница не существует
        /// </summary>
        public int? PerCurrentPage { get; set; }
        /// <summary>
        /// Количество записей на следующей странице. Null, если следующей страницы не существует
        /// </summary>
        public int? PerNextPage { get; set; }
    }
}
