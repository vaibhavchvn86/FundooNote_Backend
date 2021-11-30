using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class NoteController : ControllerBase
    {
        private readonly INoteManager manager;
        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/addnote")]
        public IActionResult AddNote([FromBody] NoteModel note)
        {
            try
            {
                string message = this.manager.AddNote(note);
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
        [HttpPost]
        [Route("api/editnote")]
        public IActionResult EditNote([FromBody] NoteModel note)
        {
            try
            {
                string message = this.manager.AddNote(note);
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
    }
}
