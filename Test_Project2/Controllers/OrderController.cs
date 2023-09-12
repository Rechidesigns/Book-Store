using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Project2.Models;
using Test_Project2.Repository;

namespace Test_Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBook_Repository book_Repository;
        private readonly ICustomer_Detail_Repository customer_Detail_Repository;
        private readonly ICart_Repository cart_Repository;
        private readonly IOrder_Repository order_Repository;
        private readonly IMapper mapper;
        public OrderController(IBook_Repository book_Repository, IMapper mapper, ICustomer_Detail_Repository customer_Detail_Repository, ICart_Repository cart_Repository, IOrder_Repository order_Repository)
        {
            this.book_Repository = book_Repository;
            this.mapper = mapper;
            this.customer_Detail_Repository = customer_Detail_Repository;
            this.cart_Repository = cart_Repository;
            this.order_Repository = order_Repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderAsync()
        {
            var order = await order_Repository.GetAllAsync();
            var orderDto = mapper.Map<List<Order_ModelDto>>(order);
            return Ok(orderDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetOrdersAsync")]
        public async Task<IActionResult> GetOrderAsync(Guid id)
        {
            var order = await order_Repository.GetAsync(id);
            if (order == null)
            {
                return BadRequest("Order Does not Exist");
            }
            else
            {
                var ordertDto = mapper.Map<Order_ModelDto>(order);
                return Ok(ordertDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(AddOrder request)
        {
            var order = new Order_Model()
            {
                Book_Id = request.Book_Id,
                Cart_Id = request.Cart_Id,
                Total_Amount = request.Total_Amount,
                Customer_Id = request.Customer_Id,
                Order_Status = "",
                //Created_On = DateTime.Now,

            };
            order = await order_Repository.AddAsync(order);
            var orderDto = new Order_ModelDto()
            {
                Id = order.Id,
                Book_Id = order.Book_Id,
                Total_Amount = order.Total_Amount,
                Customer_Id = order.Customer_Id,
                Cart_Id = order.Cart_Id,
                Order_Status = order.Order_Status,
                //reated_On = books.Created_On,
            };

            return CreatedAtAction(nameof(GetOrderAsync), new { id = orderDto.Id }, orderDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateOrderAsync(Guid id, UpdateOrder request)
        {
            var order = new Order_Model()
            {
                Order_Status = request.Order_Status,
                //Created_On = DateTime.UtcNow,

            };
            var orders = await order_Repository.UpdateAsync(id, order);
            if (orders == null)
            {
                return BadRequest("Order Does Not Exist");
            }
            else
            {
                var orderDto = new Order_ModelDto()
                {
                    Id = order.Id,
                    Customer_Id = order.Customer_Id,
                    Total_Amount = order.Total_Amount,
                    Book_Id = order.Book_Id,
                    Order_Status = order.Order_Status,
                    Cart_Id = order.Cart_Id,
                    //Created_On = books.Created_On,
                };

                return CreatedAtAction(nameof(GetOrderAsync), new { id = orderDto.Id }, orderDto);
            }
        }
    }
}
