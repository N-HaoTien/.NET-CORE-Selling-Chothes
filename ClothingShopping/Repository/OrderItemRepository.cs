using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;
namespace ClothingShopping.Repository
{
    public interface IOrderItem : IRepository<OrderItem>
    {
        public Task<IEnumerable<OrderItem>> GetListOrderIncludeOrderItembyUser(string UserId);

        public List<OrderItem> GetProductIdByOrder(int OrderId);
    }
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItem
    {

        public OrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<OrderItem>> GetListOrderIncludeOrderItembyUser(string UserId)
        {
            return await DbContext.OrderItems.Include(p => p.Order).Where(p => p.Order.UserId == UserId).ToListAsync();

        }
        public List<OrderItem> GetProductIdByOrder(int OrderId)
        {
            return DbContext.OrderItems.Where(p => p.OrderId == OrderId).ToList() ;
        }
    }
}
