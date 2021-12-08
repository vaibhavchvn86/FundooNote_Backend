// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

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
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoCollection<NoteModel> Note;
        private readonly IConfiguration configuration;
        public NoteRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Note = database.GetCollection<NoteModel>("Note");
        }
        public async Task<string> AddNote(NoteModel note)
        {
            try
            {
                await Note.InsertOneAsync(note);
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
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await Note.UpdateOneAsync(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Title, note.Title));

                    await Note.UpdateOneAsync(x => x.NoteID == note.NoteID,
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
        public async Task<string> AddReminder(string NoteID, string Reminder)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Reminder, Reminder));
                    return "Reminder Added Successfully";
                }
                return "Reminder not Added";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> RemoveReminder(string NoteID)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await Note.DeleteOneAsync(x => x.NoteID == NoteID);
                    return "Reminder Deleted Successfully";
                }
                return "Reminder not edited";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> PinnedUnPinned(string NoteID)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Pinned.Equals(false))
                    {
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, false));
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, true));
                        return "Note Pinned";
                    }
                    if (noteExist.Pinned.Equals(true))
                    {
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, false));
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
        public async Task<string> ArchiveUnArchive(string NoteID)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Archive.Equals(false))
                    {
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, false));
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, true));
                        return "Note Archived";
                    }
                    if (noteExist.Archive.Equals(true))
                    {
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, false));
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
        public async Task<string> EditColor(string NoteID, string color)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Color, color));
                    return "Color Changed Successfully";
                }
                return "Color not Changed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> ImageUpload(string noteID, IFormFile image)
        {
            try
            {
                Account account = new Account(this.configuration["CloudinaryAccount:Name"], this.configuration["CloudinaryAccount:ApiKey"], this.configuration["CloudinaryAccount:ApiSecret"]);
                var cloudinary = new Cloudinary(account);
                var uploadparams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadparams);
                string imagePath = uploadResult.Url.ToString();
                var NoteExists = Note.AsQueryable().Where(x => x.NoteID == noteID).SingleOrDefault();
                if (NoteExists != null)
                {
                    NoteExists.Image = imagePath;
                    await Note.UpdateOneAsync(x => x.NoteID == noteID,
                        Builders<NoteModel>.Update.Set(x => x.Image, NoteExists.Image));
                    return "Image Uploaded Successfully";
                }
                return "Note Does not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Trash(string NoteID)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(false))
                    {
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, true));
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
        public async Task<string> Restore(string NoteID)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await Note.UpdateOneAsync(x => x.NoteID == NoteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, false));
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
        public async Task<string> DeleteForever(string NoteID)
        {
            try
            {
                var noteExist = await Note.AsQueryable().Where(x => x.NoteID == NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await Note.DeleteOneAsync(x => x.NoteID == NoteID);
                        return "Note Deleted";
                    }
                    return "Note not deleted";
                }
                return "Note not Found";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetNotes()
        {
            try
            {
                IEnumerable<NoteModel> note = Note.AsQueryable().ToList();
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

        public IEnumerable<NoteModel> GetReminder()
        {
            try
            {
                IEnumerable<NoteModel> reminder = Note.AsQueryable().Where(x => x.Reminder != null).ToList();
                if (reminder != null)
                {
                    return reminder;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetArchive()
        {
            try
            {
                IEnumerable<NoteModel> archive = Note.AsQueryable().Where(x => x.Archive == true).ToList();
                if (archive != null)
                {
                    return archive;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetTrash()
        {
            try
            {
                IEnumerable<NoteModel> trash = Note.AsQueryable<NoteModel>().Where(x => x.Trash == true).ToList();
                if (trash != null)
                {
                    return trash;
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
