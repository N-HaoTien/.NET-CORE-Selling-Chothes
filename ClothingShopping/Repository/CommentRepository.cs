using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;

namespace ClothingShopping.Repository
{
    public interface IComment : IRepository<Comment>
    {
        public Task<IEnumerable<Comment>> GetListCommentIncludeAllUserbyProduct(int Id);

    }
    public class CommentRepository : RepositoryBase<Comment>, IComment
    {
        public ApplicationDbContext DbContext;

        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<IEnumerable<Comment>> GetListCommentIncludeAllUserbyProduct(int Id)
        {
            return await DbContext.Comments.Include(p => p.User).Where(p => p.ProductId == Id).ToListAsync();
        }

    }
}
