using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ICollaboratorRepository
    {
        Task<string> AddEmail(CollaboratorModel email);
        Task<string> DeleteEmail(CollaboratorModel email);
        IEnumerable<CollaboratorModel> GetCollaborators(string userId);

    }
}
