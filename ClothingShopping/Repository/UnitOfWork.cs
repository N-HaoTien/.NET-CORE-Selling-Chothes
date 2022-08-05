using ClothingShopping.Areas.Identity.Data;
namespace ClothingShopping.Repository

{
    public interface IUnitOfWork
    {

        public void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext DbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
