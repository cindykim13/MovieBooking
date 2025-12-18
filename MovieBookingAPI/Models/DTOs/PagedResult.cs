namespace MovieBookingAPI.Models.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}