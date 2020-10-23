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
         Task<IEnumerable<Item>> GetItems();
         Task AddItem(Item item);
         Task<bool> SaveAsync();
         Task<Item> GetItemById(int id);
         Task AddEmployee(Employee employee);
         Task<Employee> GetEmployeeById(int id);
         Task AddCustomer(Customer customer);
         Task<Customer> GetCustomerById(int customerId);
         Task<IEnumerable<Customer>> GetAllCustomers();
         Task AddIngredientCategory(IngredientCategory ingredientCategory);
         Task<IngredientCategory> GetIngredientCategoryById(int id);
         Task<IEnumerable<IngredientCategory>> GetAllIngredientCategories();
         Task<IEnumerable<Ingredient>> GetAllIngredients();
         Task<Ingredient> GetIngredientById(int id);
         Task AddIngredient(Ingredient ingredient);
    }
}