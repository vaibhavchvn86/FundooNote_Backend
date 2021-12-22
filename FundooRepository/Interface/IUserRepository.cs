// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "IUserRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Threading.Tasks;
    using FundooModels;

    public interface IUserRepository
    {
        Task<string> Register(RegisterModel user);

        Task<string> Login(LoginModel login);

        Task<string> ForgetPassword(string email);

        Task<string> ResetPassword(ResetModel password);

        string GenerateToken(string email);
    }
}
