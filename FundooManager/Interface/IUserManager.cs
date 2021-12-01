using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel user);
        Task<string> Login(LoginModel user);
        Task<string> ForgetPassword(ForgetModel email);
        Task<string> ResetPassword(ResetModel newpassword);
        string GenerateToken(string email);
    }
}
