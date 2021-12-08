// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LoginModel.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace FundooModels
{
    public class LoginModel
    {

        [Required]
        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password is not valid. Password Should be 8 Characters & contain 1 Uppercase, 1 Special character, 1 Number")]
        public string Password { get; set; }

    }
}
