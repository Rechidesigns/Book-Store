using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public interface IToken_Handler
    {
        Task<string> CreateTokenAsync(General_User users);
        Task<string> RefreshTokenAsync(string email);
    }
}
