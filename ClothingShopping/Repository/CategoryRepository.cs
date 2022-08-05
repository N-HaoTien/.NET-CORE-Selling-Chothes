using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Linq.Expressions;

namespace ClothingShopping.Repository
{
    public interface ICategory : IRepository<Category>
    {

    }
    public class CategoryRepository : RepositoryBase<Category>,ICategory
    {
        public ApplicationDbContext DbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
    }
}
