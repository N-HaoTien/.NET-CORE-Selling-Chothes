using ClothingShopping.Models;
using ClothingShopping.Repository;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace ClothingShopping.Services
{
    public interface ISizeService
    {
        public Size GetDetail(int id);
        public List<Size> GetSizebyCategoryId(int categoryId);

        public void Add(Size Size);
        public IEnumerable<Size> GetAll();
        public IEnumerable<Size> GetSizebyId(List<int> ListSizeId);

        public void AddMutipe(IEnumerable<Size> values);


        public void Update(Size Size);
        public bool CheckExistName(string name);

        public void Delete(int id);

        public void Delete(Size Size);
        public void Save();
    }
    public class SizeService : ISizeService
    {
        public ISize _Size;
        public IUnitOfWork _unitOfWork;

        public SizeService(ISize Size, IUnitOfWork unitOfWork)
        {
            _Size = Size;
            _unitOfWork = unitOfWork;
        }
        public void Add(Size Size)
        {
            _Size.Add(Size);
            Save();
        }
        public IEnumerable<Size> GetSizebyId(List<int> ListSizeId)
        {
            return _Size.GetSizebyId(ListSizeId);
        }
        public void AddMutipe(IEnumerable<Size> values)
        {
            _Size.AddMulti(values);
            Save();
        }

        public bool CheckExistName(string name)
        {
            Expression<Func<Size, bool>> where = u => u.Name == name;
            return _Size.CheckUniqueName(where);
        }

        public void Delete(int id)
        {
            _Size.Delete(id);
            Save();
        }

        public void Delete(Size Size)
        {
            _Size.Delete(Size);
            Save();
        }

        public IEnumerable<Size> GetAll()
        {
            return _Size.GetAll();
        }

        public Size GetDetail(int id)
        {
            return _Size.GetSingleById(id);
        }

        public List<Size> GetSizebyCategoryId(int categoryId)
        {
            return _Size.GetSizebyCategoryId(categoryId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Size Size)
        {
            _Size.Update(Size); 
            Save();
        }
    }
}
