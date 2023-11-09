namespace ManagerProxy2.Services.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T>? Items { get; set; }
    }
}
