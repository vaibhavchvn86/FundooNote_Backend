using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> AddEmail(CollaboratorModel email);
        Task<string> DeleteEmail(CollaboratorModel email);
        IEnumerable<CollaboratorModel> GetCollaborators(string userId);
    }
}
