using Test_Project2.Models;

namespace Test_Project2.Repository
{
    public interface IBook_Repository
    {
        Task<IEnumerable<Books_Model>> GetAllAsync();
        Task<Books_Model> GetAsync(Guid id);
        Task<Books_Model> AddAsync(Books_Model books);
        Task<Books_Model> DeleteAsync(Guid id);
        Task<Books_Model> UpdateAsync(Guid id, Books_Model books);
        Task<Books_Model> UpdateQuantityAsync(Guid id);
    }
}
