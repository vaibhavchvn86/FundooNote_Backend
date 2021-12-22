// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "ICollaboratorRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company = "BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    public interface ICollaboratorRepository
    {
        Task<CollaboratorModel> AddEmail(CollaboratorModel collaborator);

        Task<bool> DeleteEmail(string Id);

        IEnumerable<CollaboratorModel> GetCollaborators(string noteId);
    }
}
