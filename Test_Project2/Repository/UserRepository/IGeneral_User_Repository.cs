using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public interface IGeneral_User_Repository
    {
        Task<IEnumerable<General_User>> GetAllAsync();
        Task<General_User> GetAsync(Guid id);
        Task<General_User> AddAsync(General_User region, string password);
        Task<General_User> DeleteAsync(Guid id);
        Task<General_User> UpdateAsync(Guid id, General_User region);
        Task<bool> UserExistAsync(string email);
        Task<bool> UserPhoneNoExistAsync(string phoneNo);
        Task<bool> UserNameExistAsync(string userName);
        Task<General_User> AuthenticateAsync(string email, string password);
        Task<General_User> VerifyAsync(string token);
        Task<General_User> PhoneNoVerifyAsync(string otp);
        Task<General_User> ForgetPasswordAsync(string email);
        Task<General_User> RequestNewEmailTokenAsync(string email);
        Task<General_User> PasswordResetAsync(string token, string password);
        Task<General_User> UploadPicsAsync(Guid id, General_User imagePath);
        Task<General_User> TwoFactorEnabledAsync(Guid id);
        Task<General_User> CheckUserAsync(Guid id);
    }
}
