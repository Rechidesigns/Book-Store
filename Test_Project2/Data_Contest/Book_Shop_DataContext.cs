using Microsoft.EntityFrameworkCore;
using Test_Project2.Models;

namespace Test_Project2.Data_Contest
{
    public class Book_Shop_DataContext : DbContext
    {
        public Book_Shop_DataContext (DbContextOptions <Book_Shop_DataContext> options) : base (options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Books_Model> Books_Table
        {
            get;
            set;
        }
        public DbSet<Cart_Model> Cart_Table
        {
            get;
            set;
        }
        public DbSet<Customer_Details> Customer_Details_Table
        {
            get;
            set;
        }
        public DbSet<Order_Model> Order_Table
        {
            get;
            set;
        }
    }
}
