using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface IUserService
    {
        public ApplicationUser GetDetail(int id);

        public void Add(ApplicationUser ApplicationUser);

        public void Update(ApplicationUser ApplicationUser);

        public void Delete(int id);

        public void Delete(ApplicationUser ApplicationUser);
        public IEnumerable<ApplicationUser> GetListApplicationUserByGroupId(string groupId);

        public void Save();
    }
    public class UserService : IUserService
    {
        public IUser _ApplicationUser;
        public IUnitOfWork _unitOfWork;

        public UserService(IUser _ApplicationUser, IUnitOfWork unitOfWork)
        {
            _ApplicationUser = _ApplicationUser;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ApplicationUser> GetListApplicationUserByGroupId(string Id)
        {
            return _ApplicationUser.GetListUserByGroupId(Id);
        }
        public ApplicationUser GetDetail(int id)
        {
            return _ApplicationUser.GetSingleById(id);
        }

        public void Add(ApplicationUser ApplicationUser)
        {
            _ApplicationUser.Add(ApplicationUser); Save();
        }

        public void Update(ApplicationUser ApplicationUser)
        {
            _ApplicationUser.Update(ApplicationUser); Save();
        }

        public void Delete(int id)
        {
            _ApplicationUser.Delete(id); Save();
        }

        public void Delete(ApplicationUser ApplicationUser)
        {
            _ApplicationUser.Delete(ApplicationUser); Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
