// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelController.cs" Company = "BridgeLabz">
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
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LabelController class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly ILabelManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Response from this API</returns>
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

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>Response from this API</returns>
        [HttpPut]
        [Route("editlabel")]
        public async Task<IActionResult> EditLabel(string labelID, string label)
        {
            try
            {
                string message = await this.manager.EditLabel(labelID, label);
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

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Response from this API</returns>
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

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Response from this API</returns>
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

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Response from this API</returns>
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

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>Response from this API</returns>
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

        /// <summary>
        /// Gets the label by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Response from this API</returns>
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

        /// <summary>
        /// Gets the note by label identifier.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpGet]
        [Route("getnotebylabelid")]
        public IActionResult GetNoteByLabelId(string labelID)
        {
            try
            {
                IEnumerable<LabelModel> label = this.manager.GetNoteByLabelId(labelID);
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
