using ClothingShopping.Models;
using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.ViewModel;

namespace ClothingShopping.Repository
{
    public interface IUser : IRepository<ApplicationUser>
    {
        public IEnumerable<UsertoRoleViewModel> GetListUserByGroupId();
        public ApplicationUser GetDetail(string id);

    }
    public class UserRepository : RepositoryBase<ApplicationUser>, IUser
    {

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ApplicationUser GetDetail(string id)
        {
            return DbContext.Users.Find(id);
        }
        public IEnumerable<UsertoRoleViewModel> GetListUserByGroupId()
        {
            var query = (from user in DbContext.Users
                         select new
                         {
                             UserId = user.Id,
                             UserName = user.Name,
                             Email = user.Email,
                             RolesName = (from RoleUser in DbContext.UserRoles
                                          join role in DbContext.Roles on RoleUser.RoleId equals role.Id
                                          where user.Id == RoleUser.UserId
                                          select role.Name).ToList(),
                            UserClaim = (from UserClaim in DbContext.UserClaims
                                         where user.Id == UserClaim.UserId
                                         select UserClaim.ClaimType).ToList()
                         });
            return query.ToList().Select(p => new UsertoRoleViewModel()
            {
                UserId = p.UserId,
                UserName = p.UserName,
                Email = p.Email,
                RoleName = String.Join(",", p.RolesName),
                UserClaim = String.Join(",", p.UserClaim)
            });
        }
    }
}
