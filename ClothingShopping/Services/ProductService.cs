using ClothingShopping.Models;
using ClothingShopping.Repository;
using System.Linq.Expressions;

namespace ClothingShopping.Services
{
    public interface IProductService
    {
        public Product GetDetail(int id);

        public void Add(Product Product);

        public void Update(Product Product);
        public bool CheckExistName(string name);

        public void Delete(int id);

        public void Delete(Product Product);
        public Task<IEnumerable<Product>> GetListProductIncludeCategory();
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
        public async Task<IEnumerable<Product>> GetListProductIncludeCategory ()
        {
            return await _Product.GetListProductIncludeCategory();
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

        public void Update(Product Product)
        {
            _Product.Update(Product); Save();
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
    }
}
