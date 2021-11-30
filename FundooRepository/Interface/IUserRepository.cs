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
        string Login(LoginModel logindetails);
        string ForgetPassword(ForgetModel email);
        string GenerateToken(string email);
        string ResetPassword(ResetModel newpassword);
    }
}
