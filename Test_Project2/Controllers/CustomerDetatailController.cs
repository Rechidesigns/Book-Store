using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Test_Project2.Models;
using Test_Project2.Repository;
using Test_Project2.Repository.UserRepository;
using Microsoft.IdentityModel.Tokens;
using Test_Project2.Models.UserModel;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IGeneral_User_Repository general_User_Repository;
        private readonly IRole_Repository role_Repository;
        private readonly IUser_Role_Repositoty user_Role_Repositoty;
        private readonly IToken_Handler token_Handler;

        public CustomerDetatailController(IBook_Repository book_Repository, IMapper mapper, ICustomer_Detail_Repository customer_Detail_Repository, ICart_Repository cart_Repository, IOrder_Repository order_Repository, IRole_Repository role_Repository, IUser_Role_Repositoty user_Role_Repositoty, IGeneral_User_Repository general_User_Repository, IToken_Handler token_Handler)
        {
            this.book_Repository = book_Repository;
            this.mapper = mapper;
            this.customer_Detail_Repository = customer_Detail_Repository;
            this.cart_Repository = cart_Repository;
            this.order_Repository = order_Repository;
            this.role_Repository = role_Repository;
            this.user_Role_Repositoty = user_Role_Repositoty;
            this.general_User_Repository = general_User_Repository;
            this.token_Handler = token_Handler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Login_Request requset)
        {
            var user = await general_User_Repository.AuthenticateAsync(requset.Email!, requset.Password!);
            if (user != null)
            {
                if (user.EmailConfirmed == false)
                {
                    var user2 = await general_User_Repository.RequestNewEmailTokenAsync(requset.Email!);
                   // SendEmailVerificationCode(user.Email!, user2.VerificationToken!, user.SurName!);
                    string message1 = user.FirstName + " Your Book Store Account Verication Code is " + user2.VerificationToken!;

                    /* MessageResource.Create(
                                to: new PhoneNumber($"+234{phoneNo}"),
                                from: new PhoneNumber("+447862129061"),
                                body: phoneMessage,
                                client: _client);*/
                    return BadRequest("User Email Not Yet Verify");
                }
                else if (user.UserType != "User")
                {
                    return BadRequest("Not A User Account");
                }
                else
                {
                    //generate jwt token
                    var token = token_Handler.CreateTokenAsync(user);
                    var refreshToken = token_Handler.RefreshTokenAsync(user.Email!);
                    // var userId = usersRepository.Userd;
                    var userDto = new LoginDto()
                    {
                        Token = await token,
                        RefreshToken = await refreshToken,
                        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                        UserId = user.Id,
                        Email = user.Email,
                        UserType = user.UserType
                    };

                    return CreatedAtAction(nameof(GetCustomersAsync), new { id = userDto.UserId }, userDto);
                }
            }
            else
            {
                return BadRequest("User Does not Exist");
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var customer = await customer_Detail_Repository.GetAllAsync();
            var customerDto = mapper.Map<List<Customer_DetailsDto>>(customer);
            return Ok(customerDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCustomersAsync")]
        [Authorize]
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
            var check = await ValidateAddCustomerAsync(request);

            if (!check)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var generalUsers = new General_User()
                {
                    FirstName = request.First_Name,
                    SurName = request.Last_Name,
                    UserType = "User",
                    Email = request.Email,
                    PhoneNo = request.Phone_Number,
                    ImagePath = "",
                    VerificationToken = CreateRandomToken(),
                    EmailConfirmed = false,
                    CreatedAt = DateTime.Now
                };

                generalUsers = await general_User_Repository.AddAsync(generalUsers, request.Password!);

                var roles = new Roles()
                {
                    Name = "User"
                };
                roles = await role_Repository.AddAsync(roles);
                var user_Roles = new User_Roles()
                {
                    GeneralUsersId = generalUsers.Id,
                    RoleId = roles.Id,
                };
                await user_Role_Repositoty.AddAsync(user_Roles);

               // SendEmailVerificationCode(users.Email!, generalUsers.VerificationToken!, users.SurName!);
                string message1 = generalUsers.FirstName + " Your Book Store Account Verication Code is " + generalUsers.VerificationToken!;

                /* MessageResource.Create(
                            to: new PhoneNumber($"+234{phoneNo}"),
                            from: new PhoneNumber("+447862129061"),
                            body: phoneMessage,
                            client: _client);*/

                var customer = new Customer_Details()
                {
                    Id = generalUsers.Id,
                    First_Name = generalUsers.FirstName,
                    Last_Name = generalUsers.SurName,
                    Email = generalUsers.Email,
                    Phone_Number = generalUsers.PhoneNo,
                    Address_Line_1 = request.Address_Line_1,
                    Address_Line_2 = request.Address_Line_2,
                    Profile_Picture = "",
                    Created_On = DateTime.Now,

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
                    Profile_Picture = customer.Profile_Picture,
                    Created_On = customer.Created_On,
                };

                return CreatedAtAction(nameof(GetCustomersAsync), new { id = customerDto.Id }, customerDto);
            }

        }
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize]
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
                };

                return CreatedAtAction(nameof(GetCustomersAsync), new { id = customerDto.Id }, customerDto);
            }
        }

        [HttpGet]
        [Route("customer-order/{customer_Id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllCustomersOrderAsync( Guid customer_Id )
        {
            var customer = await order_Repository.GetAllAsync();
            var valid = customer.Where(x => x.Customer_Id == customer_Id);
            var orderdedatilsDto = mapper.Map<List<Order_ModelDto>>(valid);
            return Ok(orderdedatilsDto);
        }

        [HttpGet]
        [Route("customer-cart/{customer_Id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllCustomerCartAsync(Guid customer_Id)
        {
            var customer = await cart_Repository.GetAllAsync();
            var valid = customer.Where(x => x.Customer_Id == customer_Id);
            var cartsDto = mapper.Map<List<Cart_ModelDto>>(valid);
            return Ok(cartsDto);
        }

        [HttpPost("Email-Verification")]
        public async Task<IActionResult> Verify(Email_Verification verification)
        {
            var user = await general_User_Repository.VerifyAsync(verification.Token!);
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }
            else
            {
                user = await general_User_Repository.AuthenticateAsync(user.Email!, verification.Password!);
                if (user != null)
                {
                    if (user.UserType != "User")
                    {
                        return BadRequest("Not A User Account");
                    }
                    else
                    {
                        //generate jwt token
                        var token = token_Handler.CreateTokenAsync(user);
                        var refreshToken = token_Handler.RefreshTokenAsync(user.Email!);
                        // var userId = usersRepository.Userd;
                        var userDto = new LoginDto()
                        {
                            Token = await token,
                            RefreshToken = await refreshToken,
                            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                            UserId = user.Id,
                            Email = user.Email,
                            UserType = user.UserType
                        };

                        return CreatedAtAction(nameof(GetCustomersAsync), new { id = userDto.UserId }, userDto);
                    }
                }
                else
                {
                    return BadRequest("InValid Password");
                }
            }

        }


        [HttpPost("ForgotPassword-RequestToken")]
        public async Task<IActionResult> ForgotPassword(Forgot_Password_Request email)
        {
            var user = await general_User_Repository.ForgetPasswordAsync(email.Email!);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            else
            {
               // SendChangePasswordCodeEmail(email.Email!, user.PasswordResetToken!, user.SurName!);
                string message1 = user.FirstName + " Your Book Store Password Reset Code is " + user.PasswordResetToken!;

                /* MessageResource.Create(
                            to: new PhoneNumber($"+234{phoneNo}"),
                            from: new PhoneNumber("+447862129061"),
                            body: phoneMessage,
                            client: _client);*/

                return Ok($"Password Reset token has been Sent to your Email");
            }
        }

        [HttpPost("EmailVerification-RequestToken")]
        public async Task<IActionResult> EmailVerificationRequestToken(Forgot_Password_Request email)
        {
            var user = await general_User_Repository.RequestNewEmailTokenAsync(email.Email!);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            else
            {
                //SendEmailVerificationCode(email.Email!, user.VerificationToken!, user.SurName!);

                string message1 = user.FirstName + " Your Book Store Verification Code is " + user.VerificationToken!;

                /* MessageResource.Create(
                            to: new PhoneNumber($"+234{phoneNo}"),
                            from: new PhoneNumber("+447862129061"),
                            body: phoneMessage,
                            client: _client);*/

                return Ok($"New token has been Sent to your Email");
            }
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResettPassword(Reset_Password_Request request)
        {

            var user = await general_User_Repository.PasswordResetAsync(request.Token, request.Password);
            if (user == null)
            {
                return BadRequest("Invalid Token.");
            }
            else
            {
                return Ok("Password successfully reset");
            }

        }

        [Route("ProfileImage")]
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UploadFile(Add_Image_Path path)
        {


            var user2 = new General_User()
            {
                ImagePath = path.ImagePath,
            };
            var user = new Customer_Details()
            {
                Profile_Picture = path.ImagePath,
            };
            // Update detials to repository
            user2 = await general_User_Repository.UploadPicsAsync(path.UserId, user2);
            await customer_Detail_Repository.UpdateImageAsync(path.UserId, user);
            var userDto = new Upload_FileDto()
            {
                UserId = path.UserId,
                ImagePath = path.ImagePath,

            };

            return CreatedAtAction(nameof(GetCustomersAsync), new { id = userDto.UserId }, userDto);
        }

        [HttpPost("PhoneNo-Verification")]
        public async Task<IActionResult> PhoneNoVerify(string otp)
        {
            var user = await general_User_Repository.PhoneNoVerifyAsync(otp);
            if (user == null)
            {
                return BadRequest("Invalid otp token.");
            }
            else
            {
                return Ok($"PhoneNo verified!");
            }

        }

        [HttpPost("TwoFactor-Enabled")]
        public async Task<IActionResult> TwoFactorEnabled(Guid id)
        {
            var user = await general_User_Repository.TwoFactorEnabledAsync(id);
            if (user == null)
            {
                return BadRequest("User Does Not Exist");
            }
            else
            {
                return Ok($"User Two Factor Authentication Enabled");
            }
        }

        #region private methods




        private static string CreateRandomToken()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new();
            for (int i = 0; i < 5; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos)!.ToString()!)) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }
        private async Task<bool> ValidateAddCustomerAsync(AddCustomer request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $" Add User Data Is Required");
                return false;
            }

            var user = await general_User_Repository.UserExistAsync(request.Email!);

            if (user)
            {
                ModelState.AddModelError($"{nameof(request.Email)}", $"{nameof(request.Email)}  already Exist");
            }
            var userPhone = await general_User_Repository.UserPhoneNoExistAsync(request.Phone_Number!);

            if (userPhone)
            {
                ModelState.AddModelError($"{nameof(request.Phone_Number)}", $"{nameof(request.Phone_Number)}  already Exist");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }
        #endregion
    }
}
