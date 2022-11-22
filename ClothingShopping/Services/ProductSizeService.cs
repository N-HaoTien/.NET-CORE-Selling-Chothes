using ClothingShopping.Common;
using ClothingShopping.Models;
using ClothingShopping.Repository;

namespace ClothingShopping.Services
{
    public interface IProductSizeService
    {
        public ProductSize GetDetail(int id);

        public void Add(ProductSize product);
        public void AddRange(List<ProductSize> productSizes);

        public List<ProductSize> GetSizeByProductId(int id);

        public void Update(ProductSize Product);
        public bool CheckExistName(string name);

        public void Delete(int id);
        public void DeleteMulti(List<ProductSize> productSizes);

        public PagedResult<ProductSize> GetPagingProduct(GetPagedRequest request);

        public void Delete(ProductSize Product);
        public void Save();
    }
    public class ProductSizeService : IProductSizeService
    {
        public IProductSize _productSize;
        public IUnitOfWork _unitOfWork;

        public ProductSizeService(IProductSize productSize, IUnitOfWork unitOfWork)
        {
            _productSize = productSize;
            _unitOfWork = unitOfWork;
        }
        public void Add(ProductSize product)
        {
            _productSize.Add(product);
        }

        public void AddRange(List<ProductSize> productSizes)
        {
            _productSize.AddMulti(productSizes);
        }

        public bool CheckExistName(string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _productSize.Delete(id);
        }

        public void Delete(ProductSize product)
        {
            _productSize.Delete(product);
        }

        public void DeleteMulti(List<ProductSize> productSizes)
        {
            _productSize.DeleteRange(productSizes);
        }

        public ProductSize GetDetail(int id)
        {
            return _productSize.GetSingleById(id);
        }
        public PagedResult<ProductSize> GetPagingProduct(GetPagedRequest request)
        {
            throw new NotImplementedException();
        }

        public List<ProductSize> GetSizeByProductId(int id)
        {
            return _productSize.GetSizeByProductId(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductSize Product)
        {
            throw new NotImplementedException();
        }
    }
}
