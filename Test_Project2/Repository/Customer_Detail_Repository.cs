using Microsoft.EntityFrameworkCore;
using Test_Project2.Data_Contest;
using Test_Project2.Models;

namespace Test_Project2.Repository
{
    public class Customer_Detail_Repository : ICustomer_Detail_Repository
    {
        private readonly Book_Shop_DataContext bOOK_Context;
        public Customer_Detail_Repository (Book_Shop_DataContext bOOK_Context)
        {
            this.bOOK_Context = bOOK_Context;
        }
        public async Task<Customer_Details> AddAsync(Customer_Details books)
        {
            books.Id = Guid.NewGuid();
            await bOOK_Context.AddAsync(books);
            await bOOK_Context.SaveChangesAsync();
            return books;
        }

        public async Task<Customer_Details> DeleteAsync(Guid id)
        {
            var books = await bOOK_Context.Customer_Details_Table!.FirstOrDefaultAsync(x => x.Id == id);
            if (books == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                // Delete Region
                bOOK_Context.Customer_Details_Table!.Remove(books);
                await bOOK_Context.SaveChangesAsync();
                return books;
            }
        }

        public async Task<IEnumerable<Customer_Details>> GetAllAsync()
        {
            return await bOOK_Context.Customer_Details_Table!
                .OrderBy(x => x.Created_On).ToListAsync();
        }

        public async Task<Customer_Details> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await bOOK_Context.Customer_Details_Table!.
               FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Customer_Details> UpdateAsync(Guid id, Customer_Details books)
        {
            var existingCustomer = await bOOK_Context.Customer_Details_Table!.
                FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                existingCustomer.First_Name = books.First_Name;
                existingCustomer.Last_Name = books.Last_Name;
                existingCustomer.Email = books.Email;
                existingCustomer.Phone_Number = books.Phone_Number;
                existingCustomer.Address_Line_1 = books.Address_Line_1;
                existingCustomer.Address_Line_2 = books.Address_Line_2;
                await bOOK_Context.SaveChangesAsync();
                return existingCustomer;
            }
        }
    }
}
