using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface IPictureService
    {
        public IEnumerable<Picture> GetAll();
        public Picture GetDetail(int id);

        public void Add(Picture Picture);

        public void Update(Picture Picture);

        public void Delete(int id);

        public void Delete(Picture Picture);

        public void Save();
    }
    public class PictureService : IPictureService
    {
        public IPicture _Picture;
        public IUnitOfWork _unitOfWork;

        public PictureService(IComment _comment, IUnitOfWork unitOfWork)
        {
            _comment = _comment;
            _unitOfWork = unitOfWork;
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

        public void Update(Picture Picture)
        {
            _Picture.Update(Picture); Save();
        }
        public Picture GetDetail(int id)
        {
            return _Picture.GetSingleById(id);
        }
    }
}
