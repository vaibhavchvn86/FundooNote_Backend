// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "UserController.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company = "BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    /// <summary>
    /// Controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IUserManager manager;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="logger">The logger.</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Response from this API</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                this.logger.LogInformation(user.firstName + " is trying to Register");
                var response = await this.manager.Register(user);
                if (response != null)
                {
                    this.logger.LogInformation(user.firstName + " has Registered Successfully");
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Register Successful", Data = response });
                }
                else
                {
                    this.logger.LogInformation(user.firstName + " is not Registered");
                    return this.BadRequest(new { Status = false, Message = "Register Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(user.firstName + " has an Exception in Register");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Logins the specified my email.
        /// </summary>
        /// <param name="myEmail">My email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Response from this API</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                this.logger.LogInformation(login.email + " is trying to Login");
                var response = await this.manager.Login(login);
                if (response != null)
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    string eemail = database.StringGet("Email");
                    string userId = database.StringGet("UserId");

                    RegisterModel data = new RegisterModel
                    {
                        firstName = firstName,
                        lastName = lastName,
                        UserID = userId,
                        email = login.email
                    };
                    string token = this.manager.GenerateToken(login.email);
                    this.logger.LogInformation(login.email + " has Login Successfully");
                    return this.Ok(new { Status = true, Message = "Login Successfully", Data = data, Token = token });
                }
                else
                {
                    this.logger.LogInformation(login.email + " is not Logged in");
                    return this.BadRequest(new { Status = false, Message = "Login Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(login.email + " has an Exception in Logged");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Forgets the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Response from this API</returns>
        [HttpPost]
        [Route("forget")]
        public async Task<IActionResult> Forget(string email)
        {
            try
            {
                this.logger.LogInformation(email + " is trying to send reset link");
                var response = await this.manager.ForgetPassword(email);
                if (response != false)
                {
                    this.logger.LogInformation(email + " has sent Link successfully");
                    return this.Ok(new { Status = true, Message = "Link Send succesfully" });
                }
                else
                {
                    this.logger.LogInformation(email + " cannot sent Link");
                    return this.BadRequest(new { Status = false, Message = "Link not Send " });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(email + " has an Exception in Logged");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Resets the specified newpassword.
        /// </summary>
        /// <param name="newpassword">The newpassword.</param>
        /// <returns>Response from this API</returns>
        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> Reset([FromBody] ResetModel newpassword)
        {
            try
            {
                this.logger.LogInformation(newpassword.Email + "is trying to Reset Password");
                var response = await this.manager.ResetPassword(newpassword);
                if (response != null)
                {
                    this.logger.LogInformation(newpassword.Email + " has Reset Password Successfully");
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Reset Password Successfully", Data = response });
                    }
                else
                {
                    this.logger.LogInformation(newpassword.Email + " cannot not be Reset Password");
                    return this.BadRequest(new { Status = false, Message = "Reset Password unsuccessfully" });
                    }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(newpassword.Email + " has an Exception in Reset Password");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
