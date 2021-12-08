// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "RegisterModel.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FundooModels
{
    public class RegisterModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string Email {get; set;}
        [Required]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password is not valid. Password Should be 8 Character contain 1 Uppercase, 1 Special character, 1 Number")]
        public string Password { get; set; }

    }
}
