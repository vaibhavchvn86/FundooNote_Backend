// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "ICollaboratorManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    public interface ICollaboratorManager
    {
        Task<string> AddEmail(CollaboratorModel collaborator);

        Task<string> DeleteEmail(string Id);

        IEnumerable<CollaboratorModel> GetCollaborators();
    }
}
