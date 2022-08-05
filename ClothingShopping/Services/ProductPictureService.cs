using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface IProductPictureService
    {
        public ProductPicture GetDetail(int id);

        public void Add(ProductPicture ProductPicture);

        public void Update(ProductPicture ProductPicture);

        public void Delete(int id);

        public void Delete(ProductPicture ProductPicture);
        public Task<IEnumerable<ProductPicture>> GetListPicturebyProduct(int Id);

        public void Save();
    }
    public class ProductPictureService : IProductPictureService
    {
        public IProductPicture _ProductPicture;
        public IUnitOfWork _unitOfWork;

        public ProductPictureService(IProductPicture _ProductPicture, IUnitOfWork unitOfWork)
        {
            _ProductPicture = _ProductPicture;
            _unitOfWork = unitOfWork;
        }
        public ProductPicture GetDetail(int id)
        {
            return _ProductPicture.GetSingleById(id);
        }
        public Task<IEnumerable<ProductPicture>> GetListPicturebyProduct(int Id)
        {
            return _ProductPicture.GetListPicturebyProduct(Id);
        }

        public void Add(ProductPicture ProductPicture)
        {
            _ProductPicture.Add(ProductPicture);
        }

        public void Update(ProductPicture ProductPicture)
        {
            _ProductPicture.Update(ProductPicture);
        }

        public void Delete(int id)
        {
            _ProductPicture.Delete(id);
        }

        public void Delete(ProductPicture ProductPicture)
        {
            _ProductPicture.Delete(ProductPicture);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
