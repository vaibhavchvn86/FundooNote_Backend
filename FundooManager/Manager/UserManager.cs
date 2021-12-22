// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "UserManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// UserManager class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.IUserManager" />
    public class UserManager : IUserManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<RegisterModel> Register(RegisterModel user)
        {
            user.password = EncodePasswordToBase64(user.password);
            try
            {
                return await this.repository.Register(user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<RegisterModel> Login(LoginModel login)
        {
            login.password = EncodePasswordToBase64(login.password);
            try
            {
                return await this.repository.Login(login);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<bool> ForgetPassword(string Email)
        {
            try
            {
                return await this.repository.ForgetPassword(Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="newpassword">The newpassword.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<RegisterModel> ResetPassword(ResetModel newpassword)
        {
            newpassword.Password = EncodePasswordToBase64(newpassword.Password);
            try
            {
                return await this.repository.ResetPassword(newpassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encodes the password to base64.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public string GenerateToken(string email)
        {
            try
            {
                return this.repository.GenerateToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
