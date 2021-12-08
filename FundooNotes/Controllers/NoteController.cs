// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteController.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager manager;

        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("addnote")]
        public async Task<IActionResult> AddNote([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.AddNote(note);
                if (message.Equals("Note Added Successfully"))
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

        [HttpPut]
        [Route("editnote")]
        public async Task<IActionResult> EditNote([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditNote(note);
                if (message.Equals("Note Updated Successfully"))
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
        
        [HttpPut]
        [Route("addreminder")]
        public async Task<IActionResult> AddReminder(string NoteID, string Reminder)
        {
            try
            {
                string message = await this.manager.AddReminder(NoteID, Reminder);
                if (message.Equals("Reminder Added Successfully"))
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

        [HttpDelete]
        [Route("removereminder")]
        public async Task<IActionResult> RemoveReminder(string NoteID)
        {
            try
            {
                string message = await this.manager.RemoveReminder(NoteID);
                if (message.Equals("Reminder Deleted Successfully"))
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

        [HttpPut]
        [Route("pinnedunpinned")]
        public async Task<IActionResult> PinnedUnPinned(string NoteID)
        {
            try
            {
                string message = await this.manager.PinnedUnPinned(NoteID);
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

        [HttpPut]
        [Route("archiveunarchive")]
        public async Task<IActionResult> ArchiveUnArchive(string NoteID)
        {
            try
            {
                string message = await this.manager.ArchiveUnArchive(NoteID);
                if (message.Equals("Note Archived"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else if(message.Equals("Note UnArchived"))
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

        [HttpPut]
        [Route("editcolor")]
        public async Task<IActionResult> EditColor(string NoteID, string color)
        {
            try
            {
                string message = await this.manager.EditColor(NoteID, color);
                if (message.Equals("Color Changed Successfully"))
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

        [HttpPut]
        [Route("imageupload")]
        public async Task<IActionResult> ImageUpload(string NoteID, IFormFile image)
        {
            try
            {
                string message = await this.manager.ImageUpload(NoteID, image);
                if (message.Equals("Image Uploaded Successfully"))
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

        [HttpPut]
        [Route("trash")]
        public async Task<IActionResult> Trash(string NoteID)
        {
            try
            {
                string message = await this.manager.Trash(NoteID);
                if (message.Equals("Note Trashed"))
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

        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> Restore(string NoteID)
        {
            try
            {
                string message = await this.manager.Restore(NoteID);
                if (message.Equals("Note Restored"))
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

        [HttpDelete]
        [Route("deleted")]
        public async Task<IActionResult> DeleteForever(string NoteID)
        {
            try
            {
                string message = await this.manager.DeleteForever(NoteID);
                if (message.Equals("Note Deleted"))
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

        [HttpGet]
        [Route("getnotes")]
        public IActionResult GetNotes()
        {
            try
            {
                IEnumerable<NoteModel> note = this.manager.GetNotes();
                if (note != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrieved SuccessFully", Data =note });
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

        [HttpGet]
        [Route("getreminder")]
        public IActionResult GetReminder()
        {
            try
            {
                IEnumerable<NoteModel> reminder = this.manager.GetReminder();
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

        [HttpGet]
        [Route("getarchive")]
        public IActionResult GetArchive()
        {
            try
            {
                IEnumerable<NoteModel> archive = this.manager.GetArchive();
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

        [HttpGet]
        [Route("gettrash")]
        public IActionResult GetTrash()
        {
            try
            {
                IEnumerable<NoteModel> trash = this.manager.GetTrash();
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
