// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "CollaboratorController.cs" Company = "BridgeLabz">
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
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CollaboratorController class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly ICollaboratorManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Response from this API</returns>
        [HttpPost]
        [Route("addemail")]
        public async Task<IActionResult> AddEmail(CollaboratorModel collaborator)
        {
            try
            {
                string message = await this.manager.AddEmail(collaborator);
                if (message.Equals("Collaborator Added Successfully"))
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
        /// Deletes the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Response from this API</returns>
        [HttpDelete]
        [Route("deleteemail")]
        public async Task<IActionResult> DeleteEmail(string Id)
        {
            try
            {
                string message = await this.manager.DeleteEmail(Id);
                if (message.Equals("Collaborator Deleted Successfully"))
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
        /// Gets the collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Response from this API</returns>
        [HttpGet]
        [Route("getcollaborator")]
        public IActionResult GetCollaborators()
        {
            try
            {
                IEnumerable<CollaboratorModel> collaborator = this.manager.GetCollaborators();
                if (collaborator != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaborators Retrieved Successfully", Data = collaborator });
                    }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaborators not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
