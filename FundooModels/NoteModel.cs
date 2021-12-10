// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteModel.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company = "BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class NoteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NoteID { get; set; }

        [ForeignKey("RegisterModel")]
        public virtual RegisterModel RegisterModel { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }

        [DefaultValue(false)]
        public bool Pinned { get; set; }

        [DefaultValue(false)]
        public bool Archive { get; set; }

        [DefaultValue(false)]
        public bool Trash { get; set; }
        public string DeleteForever { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
    }
}
