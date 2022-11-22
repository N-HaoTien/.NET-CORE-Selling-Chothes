using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Common;
using ClothingShopping.Models;
using ClothingShopping.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;
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
        public IEnumerable<Category> GetAllByName(string Name);

        public void Add(Category appRole);

        public void Update(Category AppRole);

        public void Delete(int id);
        public bool CheckExistName(int id,string name);
        public PagedResult<Category> GetPagingCategory(GetPagedRequest request);
        public int GetCountByName(string Name);

        public void Delete(Category appRole);
        public void Save();
        public string GetCategoryName(int Id);

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
        public bool CheckExistName(int id, string name)
        {
            //Expression<Func<Category, bool>> expression = e => e.Name.Equals(category.Name);

            Expression<Func<Category, bool>> where = u => u.Name == name;

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
            Save();

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
        public IEnumerable<Category> GetAllByName(string Name)
        {
            return _category.GetAllByName(Name);
        }

        public PagedResult<Category> GetPagingCategory(GetPagedRequest request)
        {
            var query =  GetAll();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.Name.Contains(request.Keyword));
            }
            //Paging

            int TotalRow = query.Count();

            var data = query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize).ToList();

            var pagedResult = new PagedResult<Category>()
            {
                TotalCount = TotalRow,
                pageIndex = request.pageIndex,
                PageSize = request.pageSize,
                Items = data
            };
            return pagedResult;
        }

        public int GetCountByName(string Name)
        {
            return GetAllByName(Name).Count();
        }

        public string GetCategoryName(int Id)
        {
            return _category.GetCategoryName(Id);
        }
    }
}
