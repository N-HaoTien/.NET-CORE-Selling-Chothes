using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface IOrderItemService
    {
        public OrderItem GetDetail(int id);

        public void Add(OrderItem OrderItem);

        public void Update(OrderItem OrderItem);

        public void Delete(int id);

        public void Delete(OrderItem OrderItem);
        public Task<IEnumerable<OrderItem>> GetListOrderIncludeOrderItembyUser(string Id);

        public void Save();
    }
    public class OrderItemService : IOrderItemService
    {
        public IOrderItem _OrderItem;
        public IUnitOfWork _unitOfWork;

        public OrderItemService(IOrderItem _OrderItem, IUnitOfWork unitOfWork)
        {
            _OrderItem = _OrderItem;
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<OrderItem>> GetListOrderIncludeOrderItembyUser(string Id)
        {
            return _OrderItem.GetListOrderIncludeOrderItembyUser(Id);
        }
        public OrderItem GetDetail(int id)
        {
            return _OrderItem.GetSingleById(id);
        }

        public void Add(OrderItem OrderItem)
        {
            _OrderItem.Add(OrderItem); Save();
        }

        public void Update(OrderItem OrderItem)
        {
            _OrderItem.Update(OrderItem); Save();
        }

        public void Delete(int id)
        {
            _OrderItem.Delete(id); Save();
        }

        public void Delete(OrderItem OrderItem)
        {
            _OrderItem.Delete(OrderItem); Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
