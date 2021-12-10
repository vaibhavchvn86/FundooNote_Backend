// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LoginModel.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company = "BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
