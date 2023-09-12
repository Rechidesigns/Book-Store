using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Project2.Models;
using Test_Project2.Repository;

namespace Test_Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IBook_Repository book_Repository;
        private readonly ICustomer_Detail_Repository customer_Detail_Repository;
        private readonly ICart_Repository cart_Repository;
        private readonly IOrder_Repository order_Repository;
        private readonly IMapper mapper;
        public CartController(IBook_Repository book_Repository, IMapper mapper, ICustomer_Detail_Repository customer_Detail_Repository, ICart_Repository cart_Repository, IOrder_Repository order_Repository)
        {
            this.book_Repository = book_Repository;
            this.mapper = mapper;
            this.customer_Detail_Repository = customer_Detail_Repository;
            this.cart_Repository = cart_Repository;
            this.order_Repository = order_Repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCartsAsync()
        {
            var cart = await cart_Repository.GetAllAsync();
            var cartDto = mapper.Map<List<Cart_ModelDto>>(cart);
            return Ok(cartDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCartsAsync")]
        public async Task<IActionResult> GetCartAsync(Guid id)
        {
            var cart = await cart_Repository.GetAsync(id);
            if (cart == null)
            {
                return BadRequest("Cart Does not Exist");
            }
            else
            {
                var cartDto = mapper.Map<Cart_ModelDto>(cart);
                return Ok(cartDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCartAsync(AddCart request)
        {
            var cart = new Cart_Model()
            {
                Book_Id = request.Book_Id,
                Total_Amount = request.Total_Amount,
                Customer_Id = request.Customer_Id,
                //Created_On = DateTime.Now,

            };
            cart = await cart_Repository.AddAsync(cart);
            var cartDto = new Cart_ModelDto()
            {
                Id = cart.Id,
                Book_Id = cart.Book_Id,
                Total_Amount = cart.Total_Amount,
                Customer_Id = cart.Customer_Id,
                //reated_On = books.Created_On,
            };

            return CreatedAtAction(nameof(GetCartAsync), new { id = cartDto.Id }, cartDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCartrAsync(Guid id, UpdateCart request)
        {
            var cart = new Cart_Model()
            {
                Total_Amount = request.Total_Amount,
                //reated_On = DateTime.UtcNow,

            };
            var carts = await cart_Repository.UpdateAsync(id, cart);
            if (carts == null)
            {
                return BadRequest("Cart Does Not Exist");
            }
            else
            {
                var cartDto = new Cart_ModelDto()
                {
                    Id = cart.Id,
                    Customer_Id = cart.Customer_Id,
                    Total_Amount = cart.Total_Amount,
                    Book_Id = cart.Book_Id,
                    //reated_On = books.Created_On,
                };

                return CreatedAtAction(nameof(GetCartAsync), new { id = cartDto.Id }, cartDto);
            }
        }
    }
}

