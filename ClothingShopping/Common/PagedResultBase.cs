namespace ClothingShopping.Common
{
    public class PagedResultBase
    {
        public int pageIndex { get; set; }
        public int TotalCount { get; set; }
        
        public int PageSize { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double) TotalCount / PageSize;
                return (int) Math.Ceiling(pageCount);
            }
        }
    }
}
