using FundooManager.Interface;
using FundooManager.Manager;
using FundooModels;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly ILogger<UserController> logger;
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                this.logger.LogInformation(user.FirstName + " is trying to Register");
                string message = await this.manager.Register(user);
                if (message.Equals("Register Successful"))
                {
                    this.logger.LogInformation(user.FirstName + " has Registered Successfully");
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogInformation(user.FirstName + " is not Registered");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(user.FirstName + " has an Exception in Register");
                return this.NotFound(new {Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel logindetails)
        {
            try
            {
                this.logger.LogInformation(logindetails.Email + " is trying to Login");
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
                    this.logger.LogInformation(logindetails.Email + " has Login Successfully");
                    return this.Ok(new { Status = true, Message = message, Data=data, Token =token});
                }
                else
                {
                    this.logger.LogInformation(logindetails.Email + " is not Logged in");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(logindetails.Email + " has an Exception in Logged");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("forget")]
        public async Task<IActionResult> Forget([FromBody] ForgetModel email)
        {
            try
            {
                this.logger.LogInformation(email.Email + " is trying to send reset link");
                string message = await this.manager.ForgetPassword(email);
                if (message.Equals("Reset Link send to Your Email"))
                {
                    this.logger.LogInformation(email.Email + " has sent Link successfully");
                    return this.Ok(new { Status = true, Message = message });

                }
                else
                {
                    this.logger.LogInformation(email.Email + " cannot sent Link");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(email.Email + " has an Exception in Logged");
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> Reset([FromBody] ResetModel newpassword)
        {
            try
            {
                this.logger.LogInformation(newpassword.Email + "is trying to Reset Password");
                string message = await this.manager.ResetPassword(newpassword);
                if (message.Equals("Reset Password Successful"))
                {
                    this.logger.LogInformation(newpassword.Email + " has Reset Password Successfully");
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogInformation(newpassword.Email + " cannot not be Reset Password");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(newpassword.Email + " has an Exception in Reset Password");
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
