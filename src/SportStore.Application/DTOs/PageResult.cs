namespace SportStore.Application.DTOs
{
    public class PageResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
