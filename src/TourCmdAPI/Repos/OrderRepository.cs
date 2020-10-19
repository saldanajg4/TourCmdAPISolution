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

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _context.Items.ToListAsync();
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
        public async Task AddItem(Item item){
            await this._context.Items.AddAsync(item);
        }
        public async Task<bool> SaveAsync(){
            return (await this._context.SaveChangesAsync() >= 0);
        }

        public async Task<Item> GetItemById(int id)
        {
            return await this._context.Items.FirstOrDefaultAsync(item => item.ItemId == id);
        }

        public async Task AddEmployee(Employee employee)
        {
            await this._context.Employees.AddAsync(employee);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {//either one of these implementations will work
            // return await this._context.Employees.Where(emp => emp.EmployeeId == id).FirstOrDefaultAsync();
            return await this._context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await this._context.Customers.AddAsync(customer);
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await this._context.Customers
                .Where(customer => customer.CustomerId == customerId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await this._context.Customers.ToListAsync();
        }
    }
}