// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "IUserManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Threading.Tasks;
    using FundooModels;

    public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel user);

        Task<RegisterModel> Login(LoginModel login);

        Task<bool> ForgetPassword(string email);

        Task<RegisterModel> ResetPassword(ResetModel newpassword);

        string GenerateToken(string email);
    }
}
