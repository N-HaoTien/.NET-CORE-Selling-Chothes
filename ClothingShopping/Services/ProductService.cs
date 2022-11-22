using ClothingShopping.Common;
using ClothingShopping.Models;
using ClothingShopping.ViewModel;
using ClothingShopping.Repository;
using System.Linq.Expressions;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ClothingShopping.Services
{
    public interface IProductService
    {
        public Product GetDetail(int id);

        public void Add(Product Product);

        public void Update(Product Product);
        public bool CheckExistName(string name);

        public void Delete(int id);
        public PagedResult<Product> GetPagingProduct(GetPagedRequest request);
        public void Delete(Product Product);
        public Task<IEnumerable<Product>> GetListProductIncludeCategory();
        public IEnumerable<Product> GetListAllProductIncludeCategory();

        public Task<IEnumerable<Product>> GetListProductbySearchAndCategoryId(string SearchString, int? Id);
        public void Save();
    }
    public class ProductService : IProductService
    {
        public IProduct _Product;
        public IUnitOfWork _unitOfWork;

        public ProductService(IProduct Product, IUnitOfWork unitOfWork)
        {
            _Product = Product;
            _unitOfWork = unitOfWork;
        }
        //public static List<Product> dataStatic = new List<Product>();
        public async Task<IEnumerable<Product>> GetListProductIncludeCategory ()
        {
            return _Product.GetListProductIncludeCategory();
        }
        public async Task<IEnumerable<Product>> GetListProductbySearchAndCategoryId(string SearchString, int? Id)
        {
            return await _Product.GetListProductbySearchAndCategoryId(SearchString,Id);
        }
        public bool CheckExistName(string name)
        {
            Expression<Func<Product, bool>> where = u => u.Name == name;
            return _Product.CheckUniqueName(where);
        }
        public Product GetDetail(int id)
        {
            return _Product.GetSingleById(id);
        }

        public void Add(Product Product)
        {
            _Product.Add(Product); Save();
        }

        public void Update(Product product)
        {
            var productExists = GetDetail(product.Id);
            foreach (var item in productExists.Pictures)
            {
                productExists.Pictures.Remove(item);
            }
            productExists.Pictures.AddRange(product.Pictures);
            _Product.Update(product); 
            Save();
        }

        public void Delete(int id)
        {
            _Product.Delete(id); Save();
        }

        public void Delete(Product Product)
        {
            _Product.Delete(Product); Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public PagedResult<Product> GetPagingProduct(GetPagedRequest request)
        {
            var query = _Product.GetListProductIncludeCategory();
            // ListInclude Category
            #region Filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.Name.Contains(request.Keyword));
            }
            if (request.ForeignKeyId.HasValue)
            {
                query = query.Where(p => p.CategoryId.Equals(request.ForeignKeyId));
            }
            if (request.Status.HasValue)
            {
                query = query.Where(p => p.Status.Equals(request.Status));
            }
            if(request.CategoriesId != null)
            {
                query = query.Where(p => request.CategoriesId.Contains(p.CategoryId));
            }
            #endregion
            //Paging

            int TotalRow = query.Count();

            var data = query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize).ToList();

            var pagedResult = new PagedResult<Product>()
            {
                TotalCount = TotalRow,
                pageIndex = request.pageIndex,
                PageSize = request.pageSize,
                Items = data.ToList()
            };
            return pagedResult;
        }

        public IEnumerable<Product> GetListAllProductIncludeCategory()
        {
            return _Product.GetAll();
        }
    }
}
