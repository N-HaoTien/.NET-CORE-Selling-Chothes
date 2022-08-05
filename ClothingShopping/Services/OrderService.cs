using ClothingShopping.Models;
using ClothingShopping.Repository;
namespace ClothingShopping.Services
{
    public interface IOrderService
    {
        public Order GetDetail(int id);

        public void Add(Order Order);

        public void Update(Order Order);

        public void Delete(int id);

        public void Delete(Order Order);
        public Task<IEnumerable<Order>> GetListOrderbyUser(string Id);
        public Task<IEnumerable<Order>> GetListOrderbyFromToDate(DateTime From, DateTime To);

        public void Save();
    }
    public class OrderService : IOrderService
    {
        public IOrder _Order;
        public IUnitOfWork _unitOfWork;

        public OrderService(IOrder _Order, IUnitOfWork unitOfWork)
        {
            _Order = _Order;
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Order>> GetListOrderbyFromToDate(DateTime From, DateTime To)
        {
            return _Order.GetListOrderbyFromToDate(From, To);
        }
        public Task<IEnumerable<Order>> GetListOrderbyUser(string Id)
        {
            return _Order.GetListOrderbyUser(Id);
        }
        public Order GetDetail(int id)
        {
            return _Order.GetSingleById(id);
        }

        public void Add(Order Order)
        {
            _Order.Add(Order);
            Save();
        }

        public void Update(Order Order)
        {
            _Order.Update(Order); Save();
        }

        public void Delete(int id)
        {
            _Order.Delete(id); Save();
        }

        public void Delete(Order Order)
        {
            _Order.Delete(Order); Save();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
