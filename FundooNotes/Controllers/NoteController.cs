using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Authorize]
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
        [Route("api/edittitle")]
        public async Task<IActionResult> EditTitle([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditTitle(note);
                if (message.Equals("Title Updated Successfully"))
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
        [Route("api/editdescription")]
        public async Task<IActionResult> EditDescription([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditDescription(note);
                if (message.Equals("Description Updated Successfully"))
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
        [Route("api/editreminder")]
        public async Task<IActionResult> EditReminder([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditReminder(note);
                if (message.Equals("Reminder Edited Successfully"))
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
        [Route("api/editpinned")]
        public async Task<IActionResult> EditPinned([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditPinned(note);
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
        [Route("api/editarchive")]
        public async Task<IActionResult> EditArchive([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditArchive(note);
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
        [Route("api/editimage")]
        public async Task<IActionResult> EditImage([FromBody] NoteModel note)
        {
            try
            {
                string message = await this.manager.EditImage(note);
                if (message.Equals("Image Updated Successfully"))
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
                else if (message.Equals("Note Restored"))
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
    }
}
