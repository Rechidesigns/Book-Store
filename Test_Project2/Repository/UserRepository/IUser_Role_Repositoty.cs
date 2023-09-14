using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public interface IUser_Role_Repositoty
    {
        Task<User_Roles> AddAsync(User_Roles roles);
    }
}
