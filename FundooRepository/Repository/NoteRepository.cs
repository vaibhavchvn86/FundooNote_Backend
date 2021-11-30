using FundooModels;
using FundooRepository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoCollection<NoteModel> _User;

        public NoteRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _User = database.GetCollection<NoteModel>("User");
        }
        public string AddNote(NoteModel note)
        {
            try
            {
                    _User.InsertOne(note);
                    return "Note Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditNote(NoteModel note)
        {
            try
            {
                var noteExist = _User.AsQueryable().Where(x => x.NoteID == note.NoteID).FirstOrDefault();
                if (noteExist != null)
                {
                    noteExist.Title = note.Title;
                    _User.UpdateOne(x => x.Title == note.Title,
                        Builders<NoteModel>.Update.Set(x => x.Title, note.Title));
                    return "Title Updated Successfully";
                }
                return "Title does not exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
