using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using System.Data.Entity;
namespace ClothingShopping.Repository
{
    public interface IOrder : IRepository<Order>
    {
        public Task<IEnumerable<Order>> GetListOrderbyUser(string UserId);

        public Task<IEnumerable<Order>> GetListOrderbyFromToDate(DateTime From, DateTime To);


    }
    public class OrderRepository : RepositoryBase<Order>, IOrder
    {
        public ApplicationDbContext DbContext;

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetListOrderbyUser(string UserId)
        {
            return await DbContext.Orders.Include(p => p.User).Where(p => p.UserId == UserId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetListOrderbyFromToDate(DateTime From, DateTime To)
        {
            return await DbContext.Orders.Include(p => p.User).Where(p => p.CreatedDate >= From && p.CreatedDate <= To).ToListAsync();
        }

    }
}
