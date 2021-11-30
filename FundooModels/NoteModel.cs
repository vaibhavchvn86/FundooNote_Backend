﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class NoteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NoteID { get; set; }

        //[ForeignKey("_UserID")]
        //public virtual RegisterModel RegisterModel { get; set; }
        //public string _UserID { get; set; }
        
        public string Title { get; set; }

        //[Required]
        public string Description { get; set; }
        public string Reminder { get; set; }
        [DefaultValue(false)]
        public bool Pinned { get; set; }
        [DefaultValue(false)]
        public bool Archive { get; set; }
        public string Trash { get; set; }
        public string DeleteForever { get; set; }
        public string Restore { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }

    }
}