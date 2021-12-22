// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "CollaboratorRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company = "BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;
    using FundooRepository.Interface;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    /// <summary>
    /// CollaboratorRepository class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ICollaboratorRepository" />
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// The collaborator
        /// </summary>
        private readonly IMongoCollection<CollaboratorModel> Collaborator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public CollaboratorRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Collaborator = database.GetCollection<CollaboratorModel>("Collaborator");
        }

        /// <summary>
        /// Adds the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Collaborator Added Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<CollaboratorModel> AddEmail(CollaboratorModel collaborator)
        {
            try
            {
                var noteExist = await this.Collaborator.AsQueryable().Where(x => x.NoteId == collaborator.NoteId).SingleOrDefaultAsync();
                if (noteExist != null)
                {
                    var emailExist = await this.Collaborator.AsQueryable().Where(x => x.Email == collaborator.Email).FirstOrDefaultAsync();
                    if (emailExist == null)
                    {
                        await Collaborator.InsertOneAsync(collaborator);
                        return noteExist;
                    }

                    return null;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Collaborator Deleted Successfully</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<bool> DeleteEmail(string Id)
        {
            try
            {
                var noteExist = await this.Collaborator.AsQueryable().Where(x => x.CollaboratorId == Id).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await Collaborator.DeleteOneAsync(x => x.CollaboratorId == Id);
                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>All collaborators</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<CollaboratorModel> GetCollaborators(string noteId)
        {
            try
            {
                IEnumerable<CollaboratorModel> collaborator = this.Collaborator.AsQueryable().Where(x => x.NoteId == noteId).ToList();
                if (collaborator != null)
                {
                    return collaborator;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
