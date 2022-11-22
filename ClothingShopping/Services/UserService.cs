using ClothingShopping.Models;
using ClothingShopping.Repository;
using ClothingShopping.ViewModel;

namespace ClothingShopping.Services
{
    public interface IUserService
    {
        public ApplicationUser GetDetail(string id);

        public void Add(ApplicationUser ApplicationUser);

        public void Update(ApplicationUser ApplicationUser);

        public void Delete(int id);

        public void Delete(ApplicationUser ApplicationUser);
        public IEnumerable<UsertoRoleViewModel> GetListApplicationUserByGroupId();
        public IEnumerable<ApplicationUser> GetAll();

        public void Save();
    }
    public class UserService : IUserService
    {
        public IUser _ApplicationUser;
        public IUnitOfWork _unitOfWork;

        public UserService(IUser ApplicationUser, IUnitOfWork unitOfWork)
        {
            _ApplicationUser = ApplicationUser;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<UsertoRoleViewModel> GetListApplicationUserByGroupId()
        {
            return _ApplicationUser.GetListUserByGroupId();
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _ApplicationUser.GetAll();
        }
        public ApplicationUser GetDetail(string id)
        {
            return _ApplicationUser.GetDetail(id);
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
