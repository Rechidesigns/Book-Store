using Test_Project2.Models;

namespace Test_Project2.Repository
{
    public interface ICustomer_Detail_Repository
    {
        Task<IEnumerable<Customer_Details>> GetAllAsync();
        Task<Customer_Details> GetAsync(Guid id);
        Task<Customer_Details> AddAsync(Customer_Details books);
        Task<Customer_Details> DeleteAsync(Guid id);
        Task<Customer_Details> UpdateAsync(Guid id, Customer_Details books);
        Task<Customer_Details> UpdateImageAsync(Guid id, Customer_Details books);
    }
}
