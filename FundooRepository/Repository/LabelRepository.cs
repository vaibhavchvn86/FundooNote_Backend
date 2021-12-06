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
    public class LabelRepository: ILabelRepository
    {
        private readonly IMongoCollection<LabelModel> Label;
        public LabelRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Label = database.GetCollection<LabelModel>("Label");
        }

        public async Task<string> CreateLabel(LabelModel label)
        {
            try
            {
                await Label.InsertOneAsync(label);
                return "Label Created Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditLabel(LabelModel label)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == label.LabelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.UpdateOneAsync(x => x.LabelID == label.LabelID,
                          Builders<LabelModel>.Update.Set(x => x.Label, label.Label));
                    return "Label Edited Successfully";
                }
                return "Note not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> AddLabel(LabelModel label)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == label.LabelID).FirstOrDefaultAsync();
                if (labelExist == null)
                {
                    await Label.InsertOneAsync(label);
                    await Label.UpdateOneAsync(x => x.LabelID == label.LabelID,
                        Builders<LabelModel>.Update.Set(x => x.Label, label.Label));
                    return "Label Added Successfully";
                }
                else
                {
                    await Label.UpdateOneAsync(x => x.LabelID == label.LabelID,
                        Builders<LabelModel>.Update.Set(x => x.Label, label.Label));
                    return "Label Added Successfully";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteLabel(LabelModel label)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == label.LabelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    labelExist.Label = label.Label;
                    await Label.DeleteOneAsync(x => x.LabelID == label.LabelID);
                    return "Label Deleted Successfully";
                }
                return "Note not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveLabel(LabelModel label)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == label.LabelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.UpdateOneAsync(x => x.LabelID == label.LabelID,
                          Builders<LabelModel>.Update.Set(x => x.Label, label.Label));
                    return "Label Removed Successfully";
                }
                return "Note not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetLabelByNoteId(string noteId)
        {
            try
            {
                IEnumerable<LabelModel> label = Label.AsQueryable().Where(x => x.NoteID == noteId).ToList();
                if (label != null)
                {
                    return label;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetLabelByUserId(string userId)
        {
            try
            {
                IEnumerable<LabelModel> label = Label.AsQueryable().Where(x => x.NoteID == userId && x.UserID == userId).ToList();
                if (label != null)
                {
                    return label;
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
