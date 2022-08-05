using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;

namespace ClothingShopping.Repository
{
    public interface IPicture : IRepository<Picture>
    {

    }
    public class PictureRepository : RepositoryBase<Picture>, IPicture
    {
        public ApplicationDbContext DbContext;

        public PictureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
    }
}
