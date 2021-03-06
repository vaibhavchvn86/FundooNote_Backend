// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelModel.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class LabelModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LabelID { get; set; }

        [ForeignKey("RegisterModel")]
        public string UserID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("NoteModel")]
        public virtual NoteModel NoteModel { get; set; }
        public string NoteID { get; set; }
        public string Label { get; set; }
    }
}
