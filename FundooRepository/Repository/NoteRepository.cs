// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    /// <summary>
    /// NoteRepository class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.INoteRepository" />
    public class NoteRepository : INoteRepository
    {
        /// <summary>
        /// The note
        /// </summary>
        private readonly IMongoCollection<NoteModel> Note;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="configuration">The configuration.</param>
        public NoteRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Note = database.GetCollection<NoteModel>("Note");
        }

        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Note Added Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> AddNote(NoteModel note)
        {
            try
            {
                await this.Note.InsertOneAsync(note);
                return "Note Added Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Note Updated Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> EditNote(NoteModel note)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == note.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Title, note.Title));

                    await this.Note.UpdateOneAsync(x => x.NoteID == note.NoteID,
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

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Reminder Added Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> AddReminder(string noteID, string reminder)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                        Builders<NoteModel>.Update.Set(x => x.Reminder, reminder));
                    return "Reminder Added Successfully";
                }

                return "Reminder not Added";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Reminder Deleted Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> RemoveReminder(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.DeleteOneAsync(x => x.NoteID == noteID);
                    return "Reminder Deleted Successfully";
                }

                return "Reminder not edited";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pinned the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Note Pinned</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> PinnedUnPinned(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Pinned.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, false));
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, true));
                        return "Note Pinned";
                    }

                    if (noteExist.Pinned.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
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

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Note Archived</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> ArchiveUnArchive(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Archive.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, false));
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, true));
                        return "Note Archived";
                    }

                    if (noteExist.Archive.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
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

        /// <summary>
        /// Edits the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>Color Changed Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> EditColor(string noteID, string color)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
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

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Image Uploaded Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
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
                var noteExists = this.Note.AsQueryable().Where(x => x.NoteID == noteID).SingleOrDefault();
                if (noteExists != null)
                {
                    noteExists.Image = imagePath;
                    await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                        Builders<NoteModel>.Update.Set(x => x.Image, noteExists.Image));
                    return "Image Uploaded Successfully";
                }

                return "Note Does not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Trashes the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Note Trashed</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> Trash(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
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

        /// <summary>
        /// Restores the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Note Restored</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> Restore(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
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

        /// <summary>
        /// Deletes the forever.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Note Deleted</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> DeleteForever(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await this.Note.DeleteOneAsync(x => x.NoteID == noteID);
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

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <returns>All notes</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetNotes()
        {
            try
            {
                IEnumerable<NoteModel> note = this.Note.AsQueryable().ToList();
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

        /// <summary>
        /// Gets the reminder.
        /// </summary>
        /// <returns>All Reminders</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetReminder()
        {
            try
            {
                IEnumerable<NoteModel> reminder = this.Note.AsQueryable().Where(x => x.Reminder != null).ToList();
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

        /// <summary>
        /// Gets the archive.
        /// </summary>
        /// <returns>All Archive notes</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetArchive()
        {
            try
            {
                IEnumerable<NoteModel> archive = this.Note.AsQueryable().Where(x => x.Archive == true).ToList();
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

        /// <summary>
        /// Gets the trash.
        /// </summary>
        /// <returns>All trash notes</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetTrash()
        {
            try
            {
                IEnumerable<NoteModel> trash = this.Note.AsQueryable<NoteModel>().Where(x => x.Trash == true).ToList();
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
