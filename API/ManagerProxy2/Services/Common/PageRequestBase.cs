namespace ManagerProxy2.Services.Common
{
    public class PageRequestBase
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string? SortOrder { get; set; }
    }
}
