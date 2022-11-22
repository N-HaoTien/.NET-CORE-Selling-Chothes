using ClothingShopping.Areas.Identity.Data;

namespace ClothingShopping.Repository.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private ApplicationDbContext dbContext;

        public ApplicationDbContext Init()
        {
            return dbContext ?? (dbContext = new ApplicationDbContext());
        }
    }
    public interface IDbFactory
    {
        ApplicationDbContext Init();
    }
}
