using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using ClothingShopping.Repository;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ClothingShopping.Services
{
    public interface ICategoryService
    {
        public Category GetDetail(int? id);

        public IEnumerable<Category> GetAll(int page, int pageSize, out int totalRow, string filter);

        public IEnumerable<Category> GetAll();
        public bool ChangeStatusCategory(int? Id);

        public void Add(Category appRole);

        public void Update(Category AppRole);

        public void Delete(int id);
        public bool CheckExistName(int id,string name);

        public void Delete(Category appRole);
        public void Save();
    }
    public class CategoryService : ICategoryService
    {
        private ICategory _category;
        private IUnitOfWork _unitOfWork;

        public CategoryService(ICategory category, IUnitOfWork unitOfWork)
        {
            _category = category;
            _unitOfWork = unitOfWork;
        }
        public bool CheckExistName(int id,string name)
        {
            //Expression<Func<Category, bool>> expression = e => e.Name.Equals(category.Name);
            
            Expression<Func<Category, bool>> where = u => u.Name == name ;
            if (id != 0) 
            {
                where = u => u.Id != id && u.Name == name;
            }
            return _category.CheckUniqueName(where);

        }

        public void Add(Category Category)
        {
            _category.Add(Category);
            Save();

        }

        public void Update(Category Category)
        {
            _category.Update(Category);
            Save();

        }
        public Category GetDetail(int? id)
        {
            if (id.HasValue == false)
            {
                return null;
            }
            return _category.GetSingleById(id);
        }

        public IEnumerable<Category> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _category.GetMulti(x => x.Status);

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Category> GetAll()
        {
            return _category.GetAll();
        }
        public void Delete(int id)
        {
            _category.Delete(id);
        }
        public void Delete(Category Category)
        {
            _category.Delete(Category);
            Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public bool ChangeStatusCategory(int? Id)
        {
            if (Id.HasValue)
            {
                _category.ChangeStatusCategory(Id);
                return true;
            }
            return false;
        }

    }
}
