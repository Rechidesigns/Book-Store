using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public interface IRole_Repository
    {
        Task<Roles> AddAsync(Roles roles);
    }
}
