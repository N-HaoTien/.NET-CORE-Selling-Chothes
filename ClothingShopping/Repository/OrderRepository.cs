using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;
namespace ClothingShopping.Repository
{
    public interface IOrder : IRepository<Order>
    {
        public List<Order> GetListOrderbyUser();

        public Task<IEnumerable<Order>> GetListOrderbyFromToDate(DateTime From, DateTime To);


    }
    public class OrderRepository : RepositoryBase<Order>, IOrder
    {

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Order> GetListOrderbyUser()
        {
            return DbContext.Orders.OrderByDescending(p => p.CreatedDate).AsQueryable().ToList();
        }
        public async Task<IEnumerable<Order>> GetListOrderbyFromToDate(DateTime From, DateTime To)
        {
            return await DbContext.Orders.Include(p => p.User).Where(p => p.CreatedDate >= From && p.CreatedDate <= To).ToListAsync();
        }

    }
}
