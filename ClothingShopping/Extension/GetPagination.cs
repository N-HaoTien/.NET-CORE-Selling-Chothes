using ClothingShopping.Common;
using ClothingShopping.Models;

namespace ClothingShopping.Extension
{
    public class GetPagination <T> where T : class
    {
        public static PagedResult<T> GetPagingViewModel(GetPagedRequest request,List<T> data)
        {
            data.Where(p => p.Equals(request));
            var pagedResult = new PagedResult<T>()
            {
                TotalCount = data.Count(),
                pageIndex = request.pageIndex,
                PageSize = request.pageSize,
                Items = data
            };
            return pagedResult;
        }
    }
}
