using FundooModels;
using FundooRepository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoCollection<NoteModel> _User;

        public NoteRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _User = database.GetCollection<NoteModel>("User");
        }
        public string AddNote(NoteModel note)
        {
            try
            {
                    _User.InsertOne(note);
                    return "Note Added Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditTitle(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Title = note.Title;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Title, note.Title));
                    return "Title Updated Successfully";
                }
                return "Title does not exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditDescription(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Description = note.Description;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Description, note.Description));
                    return "Description Updated Successfully";
                }
                return "Description not edited";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditReminder(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Reminder = note.Reminder;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Reminder, note.Reminder));
                    return "Reminder Edited Successfully";
                }
                return "Reminder not edited";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public string EditPinned(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Pinned = note.Pinned;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Pinned, note.Pinned));
                    return "Note Pinned";
                }
                return "Note UnPinned";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditArchive(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Archive = note.Archive;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Archive, note.Archive));
                    return "Note Archived";
                }
                return "Note UnArchived";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditColor(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Color = note.Color;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Color, note.Color));
                    return "Color Changed Successfully";
                }
                return "Color not Changed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Trash(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Trash = note.Trash;
                    _User.UpdateOne(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Trash, note.Trash));
                    return "Note Trashed";
                }
                return "Note not Trashed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteForever(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        _User.DeleteOne(x => x.NoteID == note.NoteID);
                        return "Note Deleted";
                    }
                    return "note not deleted";
                }
                return "Note not Found";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
