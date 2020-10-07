using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Services;

namespace TourCmdAPI.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private OrderContext _context;
        public OrderRepository(OrderContext ctx)
        {
            _context = ctx;
        }

        public async Task<Order> GetOrderById(int id, bool includeItems = false)
        {
            if (includeItems)
                return await this._context.Orders.Include(o => o.Employee).Include(o => o.Items)
                    .Where(o => o.OrderId == id).FirstOrDefaultAsync();
            else
                return await this._context.Orders.Include(o => o.Employee)
                    .Where(o => o.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(bool includeItems = false)
        {
            if(includeItems){
                return await this._context.Orders.Include(o => o.Employee).Include(o => o.Items).ToListAsync();
            }
            else{
                return await this._context.Orders.Include(o => o.Employee).ToListAsync();
            }
        }
    }
}