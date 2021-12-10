// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelRepository.cs" Company = "BridgeLabz">
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
    /// LabelRepository class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ILabelRepository" />
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// The label
        /// </summary>
        private readonly IMongoCollection<LabelModel> Label;
        //// private readonly IMongoCollection<NoteModel> Note;
        //// private readonly IMongoCollection<NoteModel> User;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public LabelRepository(IFundooDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Label = database.GetCollection<LabelModel>("Label");
            //// Note = database.GetCollection<NoteModel>("Note");
            //// User = database.GetCollection<NoteModel>("User");
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Label Created Successfully</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public async Task<string> CreateLabel(LabelModel label)
        {
            try
            {
                await this.Label.InsertOneAsync(label);
                return "Label Created Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>Label Edited Successfully</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public async Task<string> EditLabel(string labelID, string label)
        {
            try
            {
                var labelExist = await this.Label.AsQueryable().Where(x => x.LabelID == labelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await this.Label.UpdateOneAsync(x => x.LabelID == labelID,
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

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Label Added Successfully</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public async Task<string> AddLabel(LabelModel label)
        {
            try
            {
                var labelExist = await this.Label.AsQueryable().Where(x => x.LabelID == label.LabelID).FirstOrDefaultAsync();
                if (labelExist == null)
                {
                    await this.Label.InsertOneAsync(label);
                    await this.Label.UpdateOneAsync(x => x.LabelID == label.LabelID,
                        Builders<LabelModel>.Update.Set(x => x.Label, label.Label));
                    return "Label Added Successfully";
                }
                else
                {
                    await this.Label.UpdateOneAsync(x => x.LabelID == label.LabelID,
                        Builders<LabelModel>.Update.Set(x => x.Label, label.Label));
                    return "Label Added Successfully";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>Label Deleted Successfully</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public async Task<string> DeleteLabel(string labelID)
        {
            try
            {
                var labelExist = await this.Label.AsQueryable().Where(x => x.LabelID == labelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.DeleteOneAsync(x => x.LabelID == labelID);
                    return "Label Deleted Successfully";
                }

                return "Note not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>Label Removed Successfully</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public async Task<string> RemoveLabel(string labelID)
        {
            try
            {
                var labelExist = await this.Label.AsQueryable().Where(x => x.LabelID == labelID).FirstOrDefaultAsync();
                if (labelExist != null)
                {
                    await Label.UpdateOneAsync(x => x.LabelID == labelID,
                          Builders<LabelModel>.Update.Set(x => x.LabelID, labelID));
                    return "Label Removed Successfully";
                }

                return "Label not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>All labels</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public IEnumerable<LabelModel> GetLabel()
        {
            try
            {
                IEnumerable<LabelModel> label = this.Label.AsQueryable().ToList();
                //// var label = (from p in Note.AsQueryable()
                ////              join o in Label.AsQueryable()
                ////              on p.NoteID equals o.NoteID into joinlabel
                ////              select joinlabel);
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

        /// <summary>
        /// Gets the label by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Label by note</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public IEnumerable<LabelModel> GetLabelByNoteId(string noteId)
        {
            try
            {
                IEnumerable<LabelModel> label = this.Label.AsQueryable().Where(x => x.UserID == noteId).ToList();
                //// var label = from p in User.AsQueryable()
                ////             join o in Label.AsQueryable()
                ////             on p.UserID equals o.UserID into joinednote
                ////             select joinednote;

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

        /// <summary>
        /// Gets the note by label identifier.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns> All notes by same label</returns>
        /// <exception cref="System.Exception">System Exception message</exception>
        public IEnumerable<LabelModel> GetNoteByLabelId(string labelID)
        {
            try
            {
                IEnumerable<LabelModel> label = this.Label.AsQueryable().Where(x => x.LabelID == labelID).ToList();
                //// var label = from p in User.AsQueryable()
                ////             join o in Label.AsQueryable()
                ////             on p.UserID equals o.UserID into joinednote
                ////             select joinednote;

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
