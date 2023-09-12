using Microsoft.EntityFrameworkCore;
using Test_Project2.Data_Contest;
using Test_Project2.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Book_Shop_DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Book_Store_DB")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IBook_Repository, Book_Repository>();
builder.Services.AddScoped<ICart_Repository, Cart_Repository>();
builder.Services.AddScoped<ICustomer_Detail_Repository, Customer_Detail_Repository>();
builder.Services.AddScoped<IOrder_Repository, Order_Repository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
