// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "IUserManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel user);
        Task<string> Login(string email, string password);
        Task<string> ForgetPassword(string email);
        Task<string> ResetPassword(ResetModel newpassword);
        string GenerateToken(string email);

    }
}
