using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModels;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoCollection<NoteModel> User;
        private IConfiguration configuration;
        public NoteRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            User = database.GetCollection<NoteModel>("User");
        }
        public async Task<string> AddNote(NoteModel note)
        {
            try
            {
                await User.InsertOneAsync(note);
                return "Note Added Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditNote(NoteModel note)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Title = note.Title;
                    await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Title, note.Title));

                    noteExist.Description = note.Description;
                    await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Description, note.Description));
                    return "Note Updated Successfully";
                }
                return "Note does not exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> AddReminder(NoteModel note)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Reminder = note.Reminder;
                    await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Reminder, note.Reminder));
                    return "Reminder Added Successfully";
                }
                return "Reminder not Added";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> RemoveReminder(NoteModel note)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Reminder = note.Reminder;
                    await User.DeleteOneAsync(x => x.NoteID == note.NoteID);
                    return "Reminder Deleted Successfully";
                }
                return "Reminder not edited";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> PinnedUnPinned(NoteModel note)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Pinned.Equals(false))
                    {
                        noteExist.Archive = false;
                        noteExist.Pinned = note.Pinned;
                        await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, note.Pinned));
                        return "Note Pinned";
                    }
                    if (noteExist.Pinned.Equals(true))
                    {
                        noteExist.Pinned = note.Pinned;
                        await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
        public async Task<string> ArchiveUnArchive(NoteModel note)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Archive.Equals(false))
                    {
                        noteExist.Pinned = false;
                        noteExist.Archive = noteExist.Archive;
                        await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, note.Archive));
                        return "Note Archived";
                    }
                    if (noteExist.Archive.Equals(true))
                    {
                        noteExist.Archive = note.Archive;
                        await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    noteExist.Color = note.Color;
                    await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
        public async Task<string> ImageUpload(IFormFile image, string noteID)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == noteID.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {

                    noteExist.Image = noteID.Image;
                    await User.UpdateOneAsync(x => x.NoteID == noteID.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Image, noteID.Image));

                    var Filename = image.FileName;
                    var Stream = image.OpenReadStream();
                    var cloudinary = new Cloudinary(this.configuration["CloudinaryAccount"]);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.Filename, image.Stream),
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    var uplPath = uploadResult.Url;

                    return "Image Uploaded Successfully";
                }
                return "Image not Uploaded";
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
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(false))
                    {
                        noteExist.Trash = note.Trash;
                        await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, note.Trash));
                        return "Note Trashed";
                    }
                }
                return "Note not Found";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Restore(NoteModel note)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        noteExist.Trash = note.Trash;
                        await User.UpdateOneAsync(x => x.NoteID == note.NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, note.Trash));
                        return "Note Restored";
                    }
                }
                return "Note not Found";
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
                var noteExist = await User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await User.DeleteOneAsync(x => x.NoteID == note.NoteID);
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

        public IEnumerable<NoteModel> GetNotes(string userId)
        {
            try
            {
                IEnumerable<NoteModel> note = User.AsQueryable<NoteModel>().Where(x => x.UserID == userId).ToList();
                if (note != null)
                {
                    return note;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
