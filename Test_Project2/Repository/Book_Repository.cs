using Microsoft.EntityFrameworkCore;
using Test_Project2.Data_Contest;
using Test_Project2.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Test_Project2.Repository
{
    public class Book_Repository : IBook_Repository
    {
        private readonly Book_Shop_DataContext bOOK_Context;
        public Book_Repository(Book_Shop_DataContext bOOK_Context)
        {
            this.bOOK_Context = bOOK_Context;
        }
        public async Task<Books_Model> AddAsync(Books_Model books)
        {
            books.Id = Guid.NewGuid();
            await bOOK_Context.AddAsync(books);
            await bOOK_Context.SaveChangesAsync();
            return books;
        }

        public async Task<Books_Model> DeleteAsync(Guid id)
        {
            var books = await bOOK_Context.Books_Table!.FirstOrDefaultAsync(x => x.Id == id);
            if (books == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                // Delete Region
                bOOK_Context.Books_Table!.Remove(books);
                await bOOK_Context.SaveChangesAsync();
                return books;
            }
        }

        public async Task<IEnumerable<Books_Model>> GetAllAsync()
        {
            return await bOOK_Context.Books_Table!
                .OrderBy(x => x.Created_On).ToListAsync();
        }

        public async Task<Books_Model> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await bOOK_Context.Books_Table!.
               FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Books_Model> UpdateAsync(Guid id, Books_Model books)
        {
            var existingBook = await bOOK_Context.Books_Table!.
                FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                existingBook.Books_Title = books.Books_Title;
                existingBook.Authors_Name = books.Authors_Name;
                existingBook.Cover_Image_Url = books.Cover_Image_Url;
                existingBook.Books_File = books.Books_File;
                existingBook.Published_Date = books.Published_Date;
                await bOOK_Context.SaveChangesAsync();
                return existingBook;
            }
        }

        public async Task<Books_Model> UpdateQuantityAsync(Guid id)
        {
            var existingBook = await bOOK_Context.Books_Table!.
    FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                existingBook.Books_Quantity = existingBook.Books_Quantity--;
                await bOOK_Context.SaveChangesAsync();
                return existingBook;
            }
        }
    }
}
