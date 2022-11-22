using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Linq.Expressions;
namespace ClothingShopping.Repository
{
    public interface ISize : IRepository<Size>
    {
        public List<Size> GetSizebyCategoryId(int categoryId);
        public IEnumerable<Size> GetSizebyId(List<int> ListSizeId);


    }
    public class SizeRepository : RepositoryBase<Size>, ISize
    {
        public SizeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Size> GetSizebyCategoryId(int categoryId)
        {
            return DbContext.Sizes.Where(p => p.CategoryId == categoryId).ToList();
        }

        public IEnumerable<Size> GetSizebyId(List<int> ListSizeId)
        {
            return ListSizeId.Select(p => GetSingleById(p)).ToList();
        }
    }
}
