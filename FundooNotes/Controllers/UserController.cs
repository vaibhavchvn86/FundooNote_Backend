using FundooManager.Interface;
using FundooManager.Manager;
using FundooModels;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private IConfiguration configuration;

        public UserController(IUserManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                string message = await this.manager.Register(user);
                if (message.Equals("Register Successful"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return  this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
            return this.NotFound(new {Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel logindetails)
        {
            try
            {
                string message = await this.manager.Login(logindetails);
                if (message.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    string email = database.StringGet("Email");
                    string userId = database.StringGet("UserId");

                    RegisterModel data = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserID = userId,
                        Email = email
                    };
                    string token = this.manager.GenerateToken(logindetails.Email);
                    return this.Ok(new { Status = true, Message = message, Data=data, Token =token});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/forget")]
        public async Task<IActionResult> Forget([FromBody] ForgetModel email)

        {
            try
            {
                string message = await this.manager.ForgetPassword(email);
                if (message.Equals("Reset Link send to Your Email"))
                {
                    return this.Ok(new { Status = true, Message = message });

                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpPost]
        [Route("api/reset")]
        public async Task<IActionResult> Reset([FromBody] ResetModel newpassword)

        {
            try
            {
                string message = await this.manager.ResetPassword(newpassword);
                if (message.Equals("Reset Password Successful"))
                {
                    return this.Ok(new { Status = true, Message = message });

                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
