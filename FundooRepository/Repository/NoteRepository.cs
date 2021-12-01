using FundooModels;
using FundooRepository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<string> AddNote(NoteModel note)
        {
            try
            {
                    await _User.InsertOneAsync(note);
                    return "Note Added Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditTitle(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Title = note.Title;
                    await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
        public async Task<string> EditDescription(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Description = note.Description;
                    await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
        public async Task<string> EditReminder(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Reminder = note.Reminder;
                    await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
       
        public async Task<string> EditPinned(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).SingleOrDefault();
                if (noteExist != null)
                {
                    if (noteExist.Pinned.Equals(false))
                    {
                        noteExist.Archive = false;
                        noteExist.Pinned = note.Pinned;
                        await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, note.Pinned));
                        return "Note Pinned";
                    }
                    if (noteExist.Pinned.Equals(true))
                    {
                        noteExist.Pinned = note.Pinned;
                        await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, note.Pinned));
                        return "Note UnPinned";
                    }
                }
                return "Note Doesnot Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditArchive(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    if (noteExist.Archive.Equals(false))
                    {
                        noteExist.Pinned = false;
                        noteExist.Archive = noteExist.Archive;
                        await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, note.Archive));
                        return "Note Archived";
                    }
                    if (noteExist.Archive.Equals(true))
                    {
                        noteExist.Archive = note.Archive;
                        await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, note.Archive));
                        return "Note UnArchived";
                    }
                }
                return "Note Doesnot Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditColor(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Color = note.Color;
                    await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
        public async Task<string> Trash(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(false))
                    {
                        noteExist.Trash = note.Trash;
                        await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, note.Trash));
                        return "Note Trashed";
                    }
                    if (noteExist.Trash.Equals(true))
                    {
                        noteExist.Trash = note.Trash;
                        await _User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, note.Trash));
                        return "Note Trashed";
                    }
                }
                return "Note not Trashed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteForever(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await _User.DeleteOneAsync(x => x.NoteID == note.NoteID);
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
