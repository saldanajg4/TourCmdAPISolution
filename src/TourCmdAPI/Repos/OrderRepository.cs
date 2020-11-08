using System;
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
            // return await _context.Items.Include(item => item.Ingredients)
            // .ToListAsync();
             return await _context.Items.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id, bool includeItems = false)
        {
            // if (includeItems)
            //     return await this._context.Orders.Include(o => o.Employee).Include(o => o.OrderItems)
            //         .Where(o => o.OrderId == id).FirstOrDefaultAsync();
            // else
            //     return await this._context.Orders.Include(o => o.Employee)
            //         .Where(o => o.OrderId == id).FirstOrDefaultAsync();
            if (includeItems)
                return await this._context.Orders.Include(o => o.OrderItems)
                    .Where(o => o.OrderId == id).FirstOrDefaultAsync();
            else
                return await this._context.Orders
                    .Where(o => o.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(bool includeItems = false)
        {
            // if(includeItems){
            //     return await this._context.Orders.Include(o => o.Employee).Include(o => o.OrderItems).ToListAsync();
            // }
            // else{
            //     return await this._context.Orders.Include(o => o.Employee).ToListAsync();
            // }
             if(includeItems){
                return await this._context.Orders.Include(o => o.OrderItems).ToListAsync();
            }
            else{
                return await this._context.Orders.ToListAsync();
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

        public async Task AddIngredientCategory(IngredientCategory ingredientCategory)
        {
            // var found = this._context.IngredientCategories
            //     .Where(i => i.IngredientCategoryName == ingredientCategory.IngredientCategoryName).ToList();
            // if(found.Count == 0)
            await this._context.IngredientCategories.AddAsync(ingredientCategory); 
        }

        public async Task<IngredientCategory> GetIngredientCategoryById(int id)
        {
            return await this._context.IngredientCategories
                .FirstOrDefaultAsync(ingCat => ingCat.IngredientCategoryId == id);
        }

        public async Task<IEnumerable<IngredientCategory>> GetAllIngredientCategories()
        {
            return await this._context.IngredientCategories.ToListAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await this._context.Ingredients
                .Include(ic => ic.IngredientCategory).ToListAsync();
        }

        //   public async Task<Entities.Tour> GetTourById(Guid id, bool includeShows = false){
        //     if(includeShows){
        //         return await _context.Tours.Include(t => t.Band).Include(t => t.Shows)
        //             .Where(t => t.TourId == id).FirstOrDefaultAsync();
        //     }
        //     else{
        //          return await _context.Tours.Include(b => b.Band)
        //             .Where(t => t.TourId == id).FirstOrDefaultAsync();
        //     }
            
        // }


        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await this._context.Ingredients
                .Include(ic => ic.IngredientCategory)
                .FirstOrDefaultAsync(i => i.IngredientId == id);
        }

        public async Task AddIngredient(Ingredient ingredient)
        {
            await this._context.AddAsync(ingredient);
        }

        public async Task AddOrder(Order order)
        {
            await this._context.AddAsync(order);
        }

        public async Task AddOrderItem(OrderItem item)
        {
            await this._context.AddAsync(item);
        }
    }
}