using FundooModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        Task<string> Register(RegisterModel user);
        Task<string> Login(LoginModel logindetails);
        Task<string> ForgetPassword(ForgetModel email);
        string GenerateToken(string email);
        Task<string> ResetPassword(ResetModel newpassword);
    }
}
