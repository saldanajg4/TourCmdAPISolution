using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourCmdAPI.Entities;

namespace TourCmdAPI.IRepos
{
    public interface IOrderRepository
    {
         Task<IEnumerable<Order>> GetOrders(bool includeItems = false);
         Task<Order> GetOrderById(int id, bool includeItems = false);
    }
}