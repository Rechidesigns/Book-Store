using Test_Project2.Data_Contest;
using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public class Roles_Repository : IRole_Repository
    {
        private readonly Book_Shop_DataContext book_Context;
        public Roles_Repository(Book_Shop_DataContext book_Context)
        {
            this.book_Context = book_Context;
        }
        public async Task<Roles> AddAsync(Roles roles)
        {
            roles.Id = Guid.NewGuid();
            await book_Context.Roles!.AddAsync(roles);
            await book_Context.SaveChangesAsync();
            return roles;
        }
    }
}
