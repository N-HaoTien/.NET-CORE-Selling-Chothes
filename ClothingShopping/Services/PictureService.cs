using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface IPictureService
    {
        public IEnumerable<Picture> GetAll();
        public Picture GetDetail(int id);

        public void Add(Picture Picture);
        public void AddRange(List<Picture> pictures);
        public void RemoveRange(List<Picture> pictures);

        public void Update(List<Picture> pictures);

        public void Delete(int id);

        public void Delete(Picture Picture);
        public List<Picture> GetPictureByProductId(int ProductId);

        public void Save();
    }
    public class PictureService : IPictureService
    {
        public IPicture _Picture;
        public IUnitOfWork _unitOfWork;

        public PictureService(IUnitOfWork unitOfWork, IPicture picture)
        {
            _unitOfWork = unitOfWork;
            _Picture = picture;
        }
        public IEnumerable<Picture> GetAll()
        {
            return _Picture.GetAll();
        }
        public void Delete(int id)
        {
            _Picture.Delete(id); Save();
        }
        public void Delete(Picture Picture)
        {
            _Picture.Delete(Picture); Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public void Add(Picture Picture)
        {
            _Picture.Add(Picture);
            Save();
        }

        public void Update(List<Picture> pictures)
        {
            
            RemoveRange(pictures);

            AddRange(pictures);

            Save();
        }
        public Picture GetDetail(int id)
        {
            return _Picture.GetSingleById(id);
        }

        public void AddRange(List<Picture> pictures)
        {
            _Picture.AddMulti(pictures);
        }

        public void RemoveRange(List<Picture> pictures)
        {
            _Picture.DeleteRange(pictures);
        }

        public List<Picture> GetPictureByProductId(int ProductId)
        {
            return _Picture.GetPictureByProductId(ProductId);
        }
    }
}
