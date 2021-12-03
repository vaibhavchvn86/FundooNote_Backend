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
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager manager;
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/addemail")]
        public async Task<IActionResult> AddEmail([FromBody] CollaboratorModel email)
        {
            try
            {
                string message = await this.manager.AddEmail(email);
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

        [HttpDelete]
        [Route("api/deleteemail")]
        public async Task<IActionResult> DeleteEmail([FromBody] CollaboratorModel email)
        {
            try
            {
                string message = await this.manager.DeleteEmail(email);
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

        [HttpGet]
        [Route("api/getcollaborator")]
        public  IActionResult GetCollaborators(string userId)
        {
            try
            {
                IEnumerable<CollaboratorModel> collaborator =  this.manager.GetCollaborators(userId);
                if (collaborator != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaborators Retrieved Successfully", Data= collaborator });
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
