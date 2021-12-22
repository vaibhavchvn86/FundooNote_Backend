// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteController.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// NoteController class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly INoteManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Response from this API</returns>
        [HttpPost]
        [Route("addnote")]
        public async Task<IActionResult> AddNote([FromBody] NoteModel note)
        {
            try
            {
                var response = await this.manager.AddNote(note);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Added Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("editnote")]
        public async Task<IActionResult> EditNote([FromBody] NoteModel note)
        {
            try
            {
                var response = await this.manager.EditNote(note);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Updated Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note not Updated" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="Reminder">The reminder.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("addreminder")]
        public async Task<IActionResult> AddReminder(string noteID, string Reminder)
        {
            try
            {
                var response = await this.manager.AddReminder(noteID, Reminder);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Reminder Added Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reminder not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpDelete]
        [Route("removereminder")]
        public async Task<IActionResult> RemoveReminder(string noteID)
        {
            try
            {
                var response = await this.manager.RemoveReminder(noteID);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Reminder Deleted Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reminder not Deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Pinneds the un pinned.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("pinnedunpinned")]
        public async Task<IActionResult> PinnedUnPinned(string noteID)
        {
            try
            {
                string message = await this.manager.PinnedUnPinned(noteID);
                if (message.Equals("Note Pinned"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else if (message.Equals("Note UnPinned"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Archives the un archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("archiveunarchive")]
        public async Task<IActionResult> ArchiveUnArchive(string noteID)
        {
            try
            {
                string message = await this.manager.ArchiveUnArchive(noteID);
                if (message.Equals("Note Archived"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else if (message.Equals("Note UnArchived"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edits the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("editcolor")]
        public async Task<IActionResult> EditColor(NoteModel color)
        {
            try
            {
                var response = await this.manager.EditColor(color);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Color Changed Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Color not Changed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("imageupload")]
        public async Task<IActionResult> ImageUpload(string noteID, IFormFile image)
        {
            try
            {
                var response = await this.manager.ImageUpload(noteID, image);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Image added Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Image not added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Trashes the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("trash")]
        public async Task<IActionResult> Trash(string noteID)
        {
            try
            {
                var response = await this.manager.Trash(noteID);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Trashed", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note not Trashed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Restores the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> Restore(string noteID)
        {
            try
            {
                var response = await this.manager.Restore(noteID);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Restored", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note not Restored" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the forever.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpDelete]
        [Route("deleted")]
        public async Task<IActionResult> DeleteForever(string noteID)
        {
            try
            {
                var response = await this.manager.DeleteForever(noteID);
                if (response != false)
                {
                    return this.Ok(new { Status = true, Message = "Note Deleted" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note not Deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <returns>Response from this API</returns>
        [HttpGet]
        [Route("getnotes")]
        public IActionResult GetNotes(string userId)
        {
            try
            {
                IEnumerable<NoteModel> note = this.manager.GetNotes(userId);
                if (note != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrieved SuccessFully", Data = note });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the reminder.
        /// </summary>
        /// <returns>Response from this API</returns>
        [HttpGet]
        [Route("getreminder")]
        public IActionResult GetReminder(string userId)
        {
            try
            {
                IEnumerable<NoteModel> reminder = this.manager.GetReminder(userId);
                if (reminder != null)
                {
                    return this.Ok(new { Status = true, Message = "Reminder Retrieved SuccessFully", Data = reminder });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the archive.
        /// </summary>
        /// <returns>Response from this API</returns>
        [HttpGet]
        [Route("getarchive")]
        public IActionResult GetArchive(string userId)
        {
            try
            {
                IEnumerable<NoteModel> archive = this.manager.GetArchive(userId);
                if (archive != null)
                {
                    return this.Ok(new { Status = true, Message = "Archive Notes Retrieved SuccessFully", Data = archive });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the trash.
        /// </summary>
        /// <returns>Response from this API</returns>
        [HttpGet]
        [Route("gettrash")]
        public IActionResult GetTrash(string userId)
        {
            try
            {
                IEnumerable<NoteModel> trash = this.manager.GetTrash(userId);
                if (trash != null)
                {
                    return this.Ok(new { Status = true, Message = "Trash Notes Retrieved SuccessFully", Data = trash });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
