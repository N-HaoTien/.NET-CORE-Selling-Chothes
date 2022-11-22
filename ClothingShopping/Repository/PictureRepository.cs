using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;

namespace ClothingShopping.Repository
{
    public interface IPicture : IRepository<Picture>
    {
        public List<Picture> GetPictureByProductId(int ProductId);
    }
    public class PictureRepository : RepositoryBase<Picture>, IPicture
    {

        public PictureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Picture> GetPictureByProductId(int ProductId)
        {
            return DbContext.Pictures.Where(p => p.ProductId == ProductId).ToList();
        }
    }
}
