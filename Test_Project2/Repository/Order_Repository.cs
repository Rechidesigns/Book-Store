using Microsoft.EntityFrameworkCore;
using Test_Project2.Data_Contest;
using Test_Project2.Models;

namespace Test_Project2.Repository
{
    public class Order_Repository : IOrder_Repository
    {
        private readonly Book_Shop_DataContext bOOK_Context;
        public Order_Repository(Book_Shop_DataContext bOOK_Context)
        {
            this.bOOK_Context = bOOK_Context;
        }
        public async Task<Order_Model> AddAsync(Order_Model books)
        {
            books.Id = Guid.NewGuid();
            await bOOK_Context.AddAsync(books);
            await bOOK_Context.SaveChangesAsync();
            return books;
        }

        public async Task<Order_Model> DeleteAsync(Guid id)
        {
            var books = await bOOK_Context.Order_Table!.FirstOrDefaultAsync(x => x.Id == id);
            if (books == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                // Delete Region
                bOOK_Context.Order_Table!.Remove(books);
                await bOOK_Context.SaveChangesAsync();
                return books;
            }
        }

        public async Task<IEnumerable<Order_Model>> GetAllAsync()
        {
            return await bOOK_Context.Order_Table!
                .OrderBy(x => x.Created_On).ToListAsync();
        }

        public async Task<Order_Model> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await bOOK_Context.Order_Table!.
               FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Order_Model> UpdateAsync(Guid id, Order_Model books)
        {
            var existingOrder = await bOOK_Context.Order_Table!.
                FirstOrDefaultAsync(x => x.Id == id);
            if (existingOrder == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                existingOrder.Order_Status = books.Order_Status;
                await bOOK_Context.SaveChangesAsync();
                return existingOrder;
            }
        }
    }
}
