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
        public async Task<NoteModel> AddNote(NoteModel note)
        {
            try
            {
                await this.Note.InsertOneAsync(note);
                return note;
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
        public async Task<NoteModel> EditNote(NoteModel note)
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
                    return noteExist;
                }

                return null;
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
        public async Task<NoteModel> AddReminder(string noteID, string reminder)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                        Builders<NoteModel>.Update.Set(x => x.Reminder, reminder));
                    return noteExist;
                }

                return null;
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
        public async Task<NoteModel> RemoveReminder(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.DeleteOneAsync(x => x.NoteID == noteID);
                    return noteExist;
                }

                return null;
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
                string message;
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Pinned.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, false));
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, true));
                        message = "Note Pinned";
                    }

                    if (noteExist.Pinned.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, false));
                        message = "Note UnPinned";
                    }
                }

                message =  "Note Doesnot Exist";
                return message;
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
                string message;
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Archive.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Pinned, false));
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, true));
                        message = "Note Archived";
                        return message;
                    }

                    if (noteExist.Archive.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Archive, false));
                        message = "Note UnArchived";
                        return message;
                    }
                }

                message = "Note Doesnot Exist";
                return message;
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
        public async Task<NoteModel> EditColor(NoteModel color)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == color.NoteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == color.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Color, color.Color));
                    return noteExist;
                }

                return null;
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
        public async Task<NoteModel> ImageUpload(string noteID, IFormFile image)
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
                    return noteExists;
                }

                return null;
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
        public async Task<NoteModel> Trash(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                           Builders<NoteModel>.Update.Set(x => x.Pinned, false));
                        await this.Note.UpdateOneAsync(x => x.NoteID == noteID,
                            Builders<NoteModel>.Update.Set(x => x.Trash, true));
                        return noteExist;
                    }
                }

                return null;
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
        public async Task<NoteModel> Restore(string noteID)
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
                        return noteExist;
                    }
                }

                return null;
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
        public async Task<bool> DeleteForever(string noteID)
        {
            try
            {
                var noteExist = await this.Note.AsQueryable().Where(x => x.NoteID == noteID).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    if (noteExist.Trash.Equals(true))
                    {
                        await this.Note.DeleteOneAsync(x => x.NoteID == noteID);
                        return true;
                    }

                    return false;
                }

                return false;
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
        public IEnumerable<NoteModel> GetNotes(string userId)
        {
            try
            {
                IEnumerable<NoteModel> note = this.Note.AsQueryable().Where(x => x.UserID == userId && x.Archive == false && x.Trash == false).ToList();
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
        public IEnumerable<NoteModel> GetReminder(string userId)
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
        public IEnumerable<NoteModel> GetArchive(string userId)
        {
            try
            {
                IEnumerable<NoteModel> archive = this.Note.AsQueryable().Where(x => x.Archive == true && x.Trash == false).ToList();
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
        public IEnumerable<NoteModel> GetTrash(string userId)
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
