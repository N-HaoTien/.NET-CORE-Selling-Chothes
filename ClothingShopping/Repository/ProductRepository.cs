using ClothingShopping.Models;
using ClothingShopping.Areas.Identity.Data;
using System.Data.Entity;
namespace ClothingShopping.Repository
{
    public interface IProduct : IRepository<Product>
    {
        public Task<IEnumerable<Product>> GetListProductIncludeCategory();
        public Task<IEnumerable<Product>> GetListProductbySearchAndCategoryId(string SearchString,int? Id);

    }
    public class ProductRepository : RepositoryBase<Product>,IProduct
    {
        public ApplicationDbContext DbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetListProductIncludeCategory()
        {
            return await DbContext.Products.Include(p => p.Category).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetListProductbySearchAndCategoryId(string SearchString,int? Id)
        {
            var query = DbContext.Products.Include(p => p.Category).AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(SearchString))
            {
                DbContext.Products.Include(p => p.Category).Where(p => p.Name.Contains(SearchString));
            }
            if (Id.HasValue)
            {
                DbContext.Products.Include(p => p.Category).Where(p => p.CategoryId == Id);
            }
            return await query.ToListAsync();
        }

    }
}
