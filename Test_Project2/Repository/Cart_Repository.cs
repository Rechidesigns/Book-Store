using Microsoft.EntityFrameworkCore;
using Test_Project2.Data_Contest;
using Test_Project2.Models;

namespace Test_Project2.Repository
{
     public class Cart_Repository : ICart_Repository
        {
            private readonly Book_Shop_DataContext bOOK_Context;
            public Cart_Repository(Book_Shop_DataContext bOOK_Context)
            {
                this.bOOK_Context = bOOK_Context;
            }
            public async Task<Cart_Model> AddAsync(Cart_Model books)
            {
                books.Id = Guid.NewGuid();
                await bOOK_Context.AddAsync(books);
                await bOOK_Context.SaveChangesAsync();
                return books;
            }

        public async Task<bool> BookExistAsync(Guid book_id)
        {

            {
                var user = await bOOK_Context.Cart_Table!.AnyAsync(x => x.Book_Id == book_id);
                if (user)
                {
#pragma warning disable CS8603 // Possible null reference return.
                    return true;
#pragma warning restore CS8603 // Possible null reference return.
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Cart_Model> DeleteAsync(Guid id)
            {
                var books = await bOOK_Context.Cart_Table!.FirstOrDefaultAsync(x => x.Id == id);
                if (books == null)
                {
#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
                }
                else
                {
                    // Delete Region
                    bOOK_Context.Cart_Table!.Remove(books);
                    await bOOK_Context.SaveChangesAsync();
                    return books;
                }
            }

            public async Task<IEnumerable<Cart_Model>> GetAllAsync()
            {
                return await bOOK_Context.Cart_Table!
                    .OrderBy(x => x.Created_On).ToListAsync();
            }

            public async Task<Cart_Model> GetAsync(Guid id)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await bOOK_Context.Cart_Table!.
                   FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
            }

            public async Task<Cart_Model> UpdateAsync(Guid id, Cart_Model books)
            {
                var existingCart = await bOOK_Context.Cart_Table!.
                    FirstOrDefaultAsync(x => x.Id == id);
                if (existingCart == null)
                {
#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
                }
                else
                {
                    existingCart.Book_Id = books.Book_Id;
                    existingCart.Total_Amount = books.Total_Amount;
                    existingCart.Created_On = books.Created_On;
                    await bOOK_Context.SaveChangesAsync();
                    return existingCart;
                }
            }
        }
    
}
