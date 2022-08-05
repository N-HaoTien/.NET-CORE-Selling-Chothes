using ClothingShopping.Models;
using ClothingShopping.Areas.Identity.Data;
namespace ClothingShopping.Repository
{
    public interface IUser : IRepository<ApplicationUser>
    {
        public IEnumerable<ApplicationUser> GetListUserByGroupId(string groupId);
    }
    public class UserRepository : RepositoryBase<ApplicationUser> ,IUser
    {
        public ApplicationDbContext DbContext ;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }


        public IEnumerable<ApplicationUser> GetListUserByGroupId(string groupId)
        {
            var query = from g in DbContext.Roles
                        join ug in DbContext.UserRoles
                        on g.Id equals ug.RoleId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.RoleId == groupId
                        select u;
            return query.ToList();
        }
    }
}
