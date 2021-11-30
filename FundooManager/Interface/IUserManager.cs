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
        string Login(LoginModel user);
        string ForgetPassword(ForgetModel email);
        string ResetPassword(ResetModel newpassword);
        string GenerateToken(string email);
    }
}
