// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "ICollaboratorManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> AddEmail(CollaboratorModel email);
        Task<string> DeleteEmail(string email);
        IEnumerable<CollaboratorModel> GetCollaborators(string noteId);

    }
}
