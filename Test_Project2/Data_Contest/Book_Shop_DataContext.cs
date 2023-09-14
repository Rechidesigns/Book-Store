using Microsoft.EntityFrameworkCore;
using Test_Project2.Models;
using Test_Project2.Models.UserModel;

namespace Test_Project2.Data_Contest
{
    public class Book_Shop_DataContext : DbContext
    {
        public Book_Shop_DataContext(DbContextOptions<Book_Shop_DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<User_Roles>()
         .HasOne(x => x.Roles)
         .WithMany(y => y.User_Roles)
         .HasForeignKey(x => x.RoleId);
        }
        public DbSet<General_User>? General_User { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<User_Roles>? User_Roles { get; set; }
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
