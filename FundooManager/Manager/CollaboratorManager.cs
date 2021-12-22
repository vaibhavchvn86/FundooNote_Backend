// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "CollaboratorManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// CollaboratorManager class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ICollaboratorManager" />
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// The collaborator repository
        /// </summary>
        private readonly ICollaboratorRepository collaboratorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class.
        /// </summary>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }

        /// <summary>
        /// Adds the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<CollaboratorModel> AddEmail(CollaboratorModel collaborator)
        {
            try
            {
                return await this.collaboratorRepository.AddEmail(collaborator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<bool> DeleteEmail(string Id)
        {
            try
            {
                return await this.collaboratorRepository.DeleteEmail(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<CollaboratorModel> GetCollaborators(string noteId)
        {
            try
            {
                return this.collaboratorRepository.GetCollaborators(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
