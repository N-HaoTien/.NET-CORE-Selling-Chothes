using ClothingShopping.Models;

namespace ClothingShopping.Common
{
    public class PagedResult <T> : PagedResultBase where T : class
    {
        public List<T> Items { get; set; }
    }
}
