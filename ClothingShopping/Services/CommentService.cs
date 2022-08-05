using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface ICommentService
    {
        public Comment GetDetail(int id);

        public void Add(Comment Comment);

        public void Update(Comment Comment);

        public void Delete(int id);

        public void Delete(Comment Comment);
        public Task<IEnumerable<Comment>> GetListCommentIncludeAllUserbyProduct(int Id);

        public void Save();
    }
    public class CommentService : ICommentService
    {
        public IComment _comment;
        public IUnitOfWork _unitOfWork;

        public CommentService(IComment _comment, IUnitOfWork unitOfWork)
        {
            _comment = _comment;
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Comment>> GetListCommentIncludeAllUserbyProduct(int Id)
        {
            return _comment.GetListCommentIncludeAllUserbyProduct(Id);
        }
        public Comment GetDetail(int id)
        {
            return _comment.GetSingleById(id);
        }

        public void Add(Comment Comment)
        {
            _comment.Add(Comment);
            Save();
        }

        public void Update(Comment Comment)
        {
            _comment.Update(Comment);
        }

        public void Delete(int id)
        {
            _comment.Delete(id); Save();
        }

        public void Delete(Comment Comment)
        {
            _comment.Delete(Comment); Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
