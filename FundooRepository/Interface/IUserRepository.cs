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
        Task<RegisterModel> Register(RegisterModel user);

        Task<RegisterModel> Login(LoginModel login);

        Task<bool> ForgetPassword(string email);

        Task<RegisterModel> ResetPassword(ResetModel password);

        string GenerateToken(string email);
    }
}
