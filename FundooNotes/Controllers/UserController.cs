using FundooManager.Interface;
using FundooManager.Manager;
using FundooModels;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public IActionResult Login([FromBody] LoginModel logindetails)
        {
            try
            {
                string message = this.manager.Login(logindetails);
                if (message.Equals("Login Successful"))
                {
                    string token = this.manager.GenerateToken(logindetails.Email);
                    return this.Ok(new { Status = true, Message = message, LoginData =logindetails.Email, Token =token});
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
        public IActionResult Forget([FromBody] ForgetModel email)

        {
            try
            {
                string message = this.manager.ForgetPassword(email);
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
        public IActionResult Reset([FromBody] ResetModel newpassword)

        {
            try
            {
                string message = this.manager.ResetPassword(newpassword);
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
