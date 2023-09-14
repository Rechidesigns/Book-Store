using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Test_Project2.Data_Contest;
using Test_Project2.RabbitMQ;
using Test_Project2.Repository;
using Test_Project2.Repository.UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Book_Shop_DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Book_Store_DB")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IBook_Repository, Book_Repository>();
builder.Services.AddScoped<ICart_Repository, Cart_Repository>();
builder.Services.AddScoped<ICustomer_Detail_Repository, Customer_Detail_Repository>();
builder.Services.AddScoped<IOrder_Repository, Order_Repository>();
builder.Services.AddScoped<IGeneral_User_Repository, General_User_Repository>();
builder.Services.AddScoped<IRole_Repository, Roles_Repository>();
builder.Services.AddScoped<IToken_Handler, Token_Handler>();
builder.Services.AddScoped<IUser_Role_Repositoty, User_Role_Repository>();
builder.Services.AddScoped<IRabbitMQ_Producer, RabbitMQ_Producer>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a Valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme,Array.Empty<string>() }
    });
});
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Book Store API",
        Version = "v1"
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))

    });

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
