using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Project2.Models;
using Test_Project2.Repository;

namespace Test_Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetatailController : ControllerBase
    {
        private readonly IBook_Repository book_Repository;
        private readonly ICustomer_Detail_Repository customer_Detail_Repository;
        private readonly ICart_Repository cart_Repository;
        private readonly IOrder_Repository order_Repository;
        private readonly IMapper mapper;
        public CustomerDetatailController(IBook_Repository book_Repository, IMapper mapper, ICustomer_Detail_Repository customer_Detail_Repository, ICart_Repository cart_Repository, IOrder_Repository order_Repository)
        {
            this.book_Repository = book_Repository;
            this.mapper = mapper;
            this.customer_Detail_Repository = customer_Detail_Repository;
            this.cart_Repository = cart_Repository;
            this.order_Repository = order_Repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var customer = await customer_Detail_Repository.GetAllAsync();
            var customerDto = mapper.Map<List<Customer_DetailsDto>>(customer);
            return Ok(customerDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCustomersAsync")]
        public async Task<IActionResult> GetCustomersAsync(Guid id)
        {
            var customer = await customer_Detail_Repository.GetAsync(id);
            if (customer == null)
            {
                return BadRequest("Customer Does not Exist");
            }
            else
            {
                var customerDto = mapper.Map<Customer_DetailsDto>(customer);
                return Ok(customerDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomersAsync(AddCustomer request)
        {
            var customer = new Customer_Details()
            {
                First_Name = request.First_Name,
                Last_Name = request.Last_Name,
                Email = request.Email,
                Phone_Number = request.Phone_Number,
                Address_Line_1 = request.Address_Line_1,
                Address_Line_2 = request.Address_Line_2,
                //Created_On = DateTime.Now,

            };
            customer = await customer_Detail_Repository.AddAsync(customer);
            var customerDto = new Customer_DetailsDto()
            {
                Id = customer.Id,
                First_Name = customer.First_Name,
                Last_Name = customer.Last_Name,
                Email = customer.Email,
                Phone_Number = customer.Phone_Number,
                Address_Line_1 = customer.Address_Line_1,
                Address_Line_2 = customer.Address_Line_2,
                //reated_On = books.Created_On,
            };

            return CreatedAtAction(nameof(GetCustomersAsync), new { id = customerDto.Id }, customerDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCustomerAsync(Guid id, AddCustomer request)
        {
            var customer = new Customer_Details()
            {
                First_Name = request.First_Name,
                Last_Name = request.Last_Name,
                Email = request.Email,
                Phone_Number = request.Phone_Number,
                Address_Line_1 = request.Address_Line_1,
                Address_Line_2 = request.Address_Line_2,
                //reated_On = DateTime.UtcNow,

            };
            var customers = await customer_Detail_Repository.UpdateAsync(id, customer);
            if (customers == null)
            {
                return BadRequest("Customer Does Not Exist");
            }
            else
            {
                var customerDto = new Customer_DetailsDto()
                {
                    Id = customer.Id,
                    First_Name = customer.First_Name,
                    Last_Name = customer.Last_Name,
                    Email = customer.Email,
                    Phone_Number = customer.Phone_Number,
                    Address_Line_1 = customer.Address_Line_1,
                    Address_Line_2 = customer.Address_Line_2,
                    //reated_On = books.Created_On,
                };

                return CreatedAtAction(nameof(GetCustomersAsync), new { id = customerDto.Id }, customerDto);
            }
        }

        [HttpGet]
        [Route("customer-order/{customer_Id:guid}")]
        public async Task<IActionResult> GetAllCustomersOrderAsync( Guid customer_Id )
        {
            var customer = await order_Repository.GetAllAsync();
            var valid = customer.Where(x => x.Customer_Id == customer_Id);
            var orderdedatilsDto = mapper.Map<List<Order_ModelDto>>(valid);
            return Ok(orderdedatilsDto);
        }

        [HttpGet]
        [Route("customer-cart/{customer_Id:guid}")]
        public async Task<IActionResult> GetAllCustomerCartAsync(Guid customer_Id)
        {
            var customer = await cart_Repository.GetAllAsync();
            var valid = customer.Where(x => x.Customer_Id == customer_Id);
            var cartsDto = mapper.Map<List<Cart_ModelDto>>(valid);
            return Ok(cartsDto);
        }
    }
}
