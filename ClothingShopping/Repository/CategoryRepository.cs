using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Linq.Expressions;

namespace ClothingShopping.Repository
{
    public interface ICategory : IRepository<Category>
    {
        public void ChangeStatusCategory(int? Id);
        public IEnumerable<Category> GetAllByName(string Name);

        public string GetCategoryName(int Id);
    }
    public class CategoryRepository : RepositoryBase<Category>,ICategory
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public void ChangeStatusCategory(int? Id)
        {
            var category = GetSingleById(Id);
            category.Status = !category.Status;
            Commit();
        }
        public IEnumerable<Category> GetAllByName(string Name)
        {
            Expression<Func<Category, bool>> where = u => u.Name.Contains(Name);
            return GetMany(where,null);
        }

        public string GetCategoryName(int Id)
        {
            return GetSingleById(Id).Name;
        }
    }
}
