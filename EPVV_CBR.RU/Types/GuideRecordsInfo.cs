namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о записях справочника (десериализуется)
    /// </summary>
    public class GuideRecordsInfo
    {
        /// <summary>
        /// <para>Массив записей справочника, в зависимости от его структуры</para>
        /// <para>В массиве возвращаются записи справочника со статусом не равным «удален», не более 100 за один запрос</para>
        /// </summary>
        public List<object> Items { get; set; } = default!;
        /// <summary>
        /// Информация о пагинации
        /// </summary>
        public PaginationInfo PaginationInfo { get; set; } = default!;
    }
}
