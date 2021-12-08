// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "CollaboratorRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooModels;
using FundooRepository.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository: ICollaboratorRepository
    {
        private readonly IMongoCollection<CollaboratorModel> Collaborator;

        public CollaboratorRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Collaborator = database.GetCollection<CollaboratorModel>("Collaborator");
        }

        public async Task<string> AddEmail(CollaboratorModel email)
        {
            try
            {
                var noteExist = await Collaborator.AsQueryable().Where(x => x.Email == email.Email).FirstOrDefaultAsync();
                if (noteExist == null)
                {
                    await Collaborator.InsertOneAsync(email);
                    return "Collaborator Added Successfully";
                }
                return "Collaborator Already Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteEmail(string email)
        {
            try
            {
                var noteExist = await Collaborator.AsQueryable().Where(x => x.Email == email).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await Collaborator.DeleteOneAsync(x => x.Email == email);
                    return "Collaborator Deleted Successfully";
                }
                return "Collaborator not Found";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CollaboratorModel> GetCollaborators(string noteId)
        {
            try
            {
                IEnumerable<CollaboratorModel> collaborator = Collaborator.AsQueryable().Where(x => x.NoteId == noteId).ToList();
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
