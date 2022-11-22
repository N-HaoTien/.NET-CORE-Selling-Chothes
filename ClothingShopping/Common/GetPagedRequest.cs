using ClothingShopping.Models;

namespace ClothingShopping.Common
{
    public class GetPagedRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int ?Id { get; set; }
        public int? ForeignKeyId { get; set; }
        public bool? Status { get; set; }

        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public List<int>? CategoriesId { get; set; }

    }
}
