using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;

namespace ClothingShopping.Repository
{
    public interface IProductSize : IRepository<ProductSize>
    {
        public List<ProductSize> GetSizeByProductId(int id);
    }
    public class ProductSizeRepository : RepositoryBase<ProductSize>, IProductSize
    {

        public ProductSizeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public List<ProductSize> GetSizeByProductId(int id)
        {
            return DbContext.ProductSizes.Where(p => p.ProductId == id).ToList();
        }
    }
}
