// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelController.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager manager;
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("createlabel")]
        public async Task<IActionResult> CreateLabel([FromBody] LabelModel label)
        {
            try
            {
                string message = await this.manager.CreateLabel(label);
                if (message.Equals("Label Created Successfully"))
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
        [Route("editlabel")]
        public async Task<IActionResult> EditLabel(string LabelID, string label)
        {
            try
            {
                string message = await this.manager.EditLabel(LabelID, label);
                if (message.Equals("Label Edited Successfully"))
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
        [Route("addlabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelModel label)
        {
            try
            {
                string message = await this.manager.AddLabel(label);
                if (message.Equals("Label Added Successfully"))
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
        [Route("removelabel")]
        public async Task<IActionResult> RemoveLabel(string label)
        {
            try
            {
                string message = await this.manager.RemoveLabel(label);
                if (message.Equals("Label Removed Successfully"))
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
        [Route("deletelabel")]
        public async Task<IActionResult> DeleteLabel(string label)
        {
            try
            {
                string message = await this.manager.DeleteLabel(label);
                if (message.Equals("Label Deleted Successfully"))
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
        [Route("getlabel")]
        public IActionResult GetLabel()
        {
            try
            {
                IEnumerable<LabelModel> label = this.manager.GetLabel();
                if (label != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Labels Retrieved SuccessFully", Data = label });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Labels not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getlabelbynoteid")]
        public IActionResult GetLabelByNoteId(string noteId)
        {
            try
            {
                IEnumerable<LabelModel> label = this.manager.GetLabelByNoteId(noteId);
                if (label != null)
                {
                    return this.Ok(new { Status = true, Message = "All Labels Retrieved SuccessFully", Data = label });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Labels not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getnotebylabelid")]
        public IActionResult GetNoteByLabelId(string LabelID)
        {
            try
            {
                IEnumerable<LabelModel> label = this.manager.GetNoteByLabelId(LabelID);
                if (label != null)
                {
                    return this.Ok(new { Status = true, Message = "All Note Retrieved by Label SuccessFully", Data = label });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Labels not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
