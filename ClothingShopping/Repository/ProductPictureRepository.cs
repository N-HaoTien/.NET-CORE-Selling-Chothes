using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;
namespace ClothingShopping.Repository
{
    public interface IProductPicture : IRepository<ProductPicture>
    {
        public Task<IEnumerable<ProductPicture>> GetListPicturebyProduct(int Id);

    }
    public class ProductPictureRepository : RepositoryBase<ProductPicture>, IProductPicture
    {
        public ApplicationDbContext DbContext;

        public ProductPictureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<IEnumerable<ProductPicture>> GetListPicturebyProduct(int Id)
        {
            return await DbContext.ProductPictures.Include(p => p.Product).Include(p => p.Picture).Where(p => p.ProductId == Id).ToListAsync();
        }
    }
}
