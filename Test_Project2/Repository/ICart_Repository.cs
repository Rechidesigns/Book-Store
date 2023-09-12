using Test_Project2.Models;

namespace Test_Project2.Repository
{
    public interface ICart_Repository
    {
        Task<IEnumerable<Cart_Model>> GetAllAsync();
        Task<Cart_Model> GetAsync(Guid id);
        Task<Cart_Model> AddAsync(Cart_Model books);
        Task<Cart_Model> DeleteAsync(Guid id);
        Task<Cart_Model> UpdateAsync(Guid id, Cart_Model books);
    }
}
