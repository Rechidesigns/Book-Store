using Test_Project2.Models;

namespace Test_Project2.Repository
{
    public interface IOrder_Repository
    {
        Task<IEnumerable<Order_Model>> GetAllAsync();
        Task<Order_Model> GetAsync(Guid id);
        Task<Order_Model> AddAsync(Order_Model books);
        Task<Order_Model> DeleteAsync(Guid id);
        Task<Order_Model> UpdateAsync(Guid id, Order_Model books);
        Task<bool> CartExistAsync(Guid cart_id);
    }
}
