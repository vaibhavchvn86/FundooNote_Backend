using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class LabelModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LabelID { get; set; }

        [ForeignKey("RegisterModel")]
        public virtual RegisterModel RegisterModel { get; set; }
        public string UserID { get; set; }
        [ForeignKey("NoteModel")]
        public virtual NoteModel NoteModel { get; set; }
        public string? NoteID { get; set; }
        public string Label { get; set; }
    }
}
