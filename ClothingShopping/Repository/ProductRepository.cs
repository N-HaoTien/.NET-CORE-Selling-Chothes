using ClothingShopping.Models;
using ClothingShopping.Areas.Identity.Data;
using System.Data.Entity;
using Syncfusion.EJ2.Linq;

namespace ClothingShopping.Repository
{
    public interface IProduct : IRepository<Product>
    {
        public IEnumerable<Product> GetListProductIncludeCategory();
        public string GetNameCategory(int Id);

        public Task<IEnumerable<Product>> GetListProductbySearchAndCategoryId(string SearchString,int? Id);
        public Task<IEnumerable<Product>> GetListProductbySearchAndListCategory(string SearchString, List<int> ListInt);
    }
    public class ProductRepository : RepositoryBase<Product>,IProduct
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        public IEnumerable<Product> GetListProductIncludeCategory()
        {
            return  DbContext.Products.Include(p => p.Category).OrderByDescending(p => p.CreatedDate).AsNoTracking();
        }
        public async Task<IEnumerable<Product>> GetListProductbySearchAndCategoryId(string SearchString,int? Id)
        {
            var query = DbContext.Products.Include(p => p.Category).AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(SearchString))
            {
                query.Where(p => p.Name.Contains(SearchString));
            }
            if (Id.HasValue && Id.Value > 0)
            {
                query.Where(p => p.CategoryId == Id);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetListProductbySearchAndListCategory(string SearchString, List<int> ListInt)
        {
            var query = DbContext.Products.Include(p => p.Category).AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(SearchString))
            {
                query.Where(p => p.Name.Contains(SearchString));
            }
            if (ListInt.Any())
            {
                query.Where(p => ListInt.Contains(p.Id));
            }
            return await query.ToListAsync();
        }

        public string GetNameCategory(int Id)
        {
            return DbContext.Products.Include(p => p.Category.Name).FirstOrDefault(p => p.CategoryId == Id).CategoryName;
        }
    }
}
