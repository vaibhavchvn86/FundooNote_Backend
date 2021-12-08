// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelRepository.cs" Company = "BridgeLabz">
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
    public class LabelRepository: ILabelRepository
    {
        private readonly IMongoCollection<LabelModel> Label;
        private readonly IMongoCollection<NoteModel> Note;
        private readonly IMongoCollection<NoteModel> User;
        public LabelRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Label = database.GetCollection<LabelModel>("Label");
            Note = database.GetCollection<NoteModel>("Note");
            User = database.GetCollection<NoteModel>("User");
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

        public async Task<string> EditLabel(string LabelID, string label)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == LabelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.UpdateOneAsync(x => x.LabelID == LabelID,
                          Builders<LabelModel>.Update.Set(x => x.Label, label));
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

        public async Task<string> DeleteLabel(string LabelID)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == LabelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.DeleteOneAsync(x => x.LabelID == LabelID);
                    return "Label Deleted Successfully";
                }
                return "Note not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveLabel(string LabelID)
        {
            try
            {
                var labelExist = await Label.AsQueryable().Where(x => x.LabelID == LabelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.UpdateOneAsync(x => x.LabelID == LabelID,
                          Builders<LabelModel>.Update.Set(x => x.LabelID, LabelID));
                    return "Label Removed Successfully";
                }
                return "Note not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetLabel()
        {
            try
            {
                IEnumerable<LabelModel> label = Label.AsQueryable().ToList();
                ////var label = (from p in Note.AsQueryable()
                ////             join o in Label.AsQueryable()
                ////             on p.NoteID equals o.NoteID into joinlabel
                ////             select joinlabel);
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

        public IEnumerable<LabelModel> GetLabelByNoteId(string NoteId)
        {
            try
            {
                IEnumerable<LabelModel> label = Label.AsQueryable().Where(x => x.UserID == NoteId).ToList();
                ////var label = from p in User.AsQueryable()
                ////            join o in Label.AsQueryable()
                ////            on p.UserID equals o.UserID into joinednote
                ////            select joinednote;

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

        public IEnumerable<LabelModel> GetNoteByLabelId(string LabelID)
        {
            try
            {
                IEnumerable<LabelModel> label = Label.AsQueryable().Where(x => x.LabelID == LabelID).ToList();
                ////var label = from p in User.AsQueryable()
                ////            join o in Label.AsQueryable()
                ////            on p.UserID equals o.UserID into joinednote
                ////            select joinednote;

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
