using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    //[Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager manager;
        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addnote")]
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
        [Route("api/editnote")]
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
        [Route("api/addreminder")]
        public async Task<IActionResult> AddReminder([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.AddReminder(note);
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
        [Route("api/removereminder")]
        public async Task<IActionResult> RemoveReminder([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.RemoveReminder(note);
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
        [Route("api/pinnedunpinned")]
        public async Task<IActionResult> PinnedUnPinned([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.PinnedUnPinned(note);
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
        [Route("api/archiveunarchive")]
        public async Task<IActionResult> ArchiveUnArchive([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.ArchiveUnArchive(note);
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
        [Route("api/editcolor")]
        public async Task<IActionResult> EditColor([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditColor(note);
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
        [Route("api/imageupload")]
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
        [Route("api/trash")]
        public async Task<IActionResult> Trash([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.Trash(note);
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
        [Route("api/restore")]
        public async Task<IActionResult> Restore([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.Trash(note);
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
        [Route("api/deleted")]
        public async Task<IActionResult> DeleteForever([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.DeleteForever(note);
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
        [Route("api/getnotes")]
        public IActionResult GetNotes(string userId)
        {
            try
            {
                IEnumerable<NoteModel> note = this.manager.GetNotes(userId);
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
    }
}
