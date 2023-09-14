using Test_Project2.Data_Contest;
using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public class User_Role_Repository : IUser_Role_Repositoty
    {
        private readonly Book_Shop_DataContext book_Context;
        public User_Role_Repository(Book_Shop_DataContext book_Context)
        {
            this.book_Context = book_Context;
        }

        public async Task<User_Roles> AddAsync(User_Roles roles)
        {
            roles.Id = Guid.NewGuid();
            await book_Context.User_Roles!.AddAsync(roles);
            await book_Context.SaveChangesAsync();
            return roles;
        }
    }
}
