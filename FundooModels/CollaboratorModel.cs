// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "CollaboratorModel.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class CollaboratorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CollaboratorId { get; set; }

        [ForeignKey("NoteModel")]
        public string NoteId { get; set; }
        public virtual NoteModel NoteModel { get; set; }

        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string Email { get; set; }

        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string DeleteEmail { get; set; }
    }
}
