using FundooModels;
using FundooRepository.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository: ICollaboratorRepository
    {
        private readonly IMongoCollection<CollaboratorModel> User;

        public CollaboratorRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            User = database.GetCollection<CollaboratorModel>("User");
        }
        public async Task<string> AddEmail(CollaboratorModel email)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.Email == email.Email).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await User.InsertOneAsync(email);
                    return "Collaborator Added Successfully";
                }
                return "Collaborator not Added";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteEmail(CollaboratorModel email)
        {
            try
            {
                var noteExist = await User.AsQueryable().Where(x => x.Email == email.Email).FirstOrDefaultAsync();
                if (noteExist != null)
                {
                    await User.DeleteOneAsync(x => x.Email == email.Email);
                    return "Collaborator Deleted Successfully";
                }
                return "Collaborator not Found";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CollaboratorModel> GetCollaborators(string userId)
        {
            try
            {
                IEnumerable<CollaboratorModel> collaborator = User.AsQueryable<CollaboratorModel>().Where(x => x.NoteId == userId).ToList();
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
