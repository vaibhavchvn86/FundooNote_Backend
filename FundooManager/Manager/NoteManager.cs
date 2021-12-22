// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// NoteManager class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.INoteManager" />
    public class NoteManager : INoteManager
    {
        /// <summary>
        /// The note repository
        /// </summary>
        private readonly INoteRepository NoteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteManager"/> class.
        /// </summary>
        /// <param name="NoteRepository">The note repository.</param>
        public NoteManager(INoteRepository NoteRepository)
        {
            this.NoteRepository = NoteRepository;
        }

        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> AddNote(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.AddNote(note);
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
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> EditNote(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> AddReminder(string noteID, string reminder)
        {
            try
            {
                return await this.NoteRepository.AddReminder(noteID, reminder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> RemoveReminder(string noteID)
        {
            try
            {
                return await this.NoteRepository.RemoveReminder(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pinneds the un pinned.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> PinnedUnPinned(string noteID)
        {
            try
            {
                return await this.NoteRepository.PinnedUnPinned(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Archives the un archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<string> ArchiveUnArchive(string noteID)
        {
            try
            {
                return await this.NoteRepository.ArchiveUnArchive(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> EditColor(NoteModel color)
        {
            try
            {
                return await this.NoteRepository.EditColor(color);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> ImageUpload(string noteID, IFormFile image)
        {
            try
            {
                return await this.NoteRepository.ImageUpload(noteID, image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Trashes the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> Trash(string noteID)
        {
            try
            {
                return await this.NoteRepository.Trash(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Restores the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<NoteModel> Restore(string noteID)
        {
            try
            {
                return await this.NoteRepository.Restore(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the forever.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<bool> DeleteForever(string noteID)
        {
            try
            {
                return await this.NoteRepository.DeleteForever(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetNotes(string userId)
        {
            try
            {
                return this.NoteRepository.GetNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the archive.
        /// </summary>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetArchive(string userId)
        {
            try
            {
                return this.NoteRepository.GetArchive(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the trash.
        /// </summary>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetTrash(string userId)
        {
            try
            {
                return this.NoteRepository.GetTrash(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the reminder.
        /// </summary>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<NoteModel> GetReminder(string userId)
        {
            try
            {
                return this.NoteRepository.GetReminder(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
