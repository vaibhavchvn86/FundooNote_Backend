// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelManager.cs" Company = "BridgeLabz">
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
    /// LabelManager class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ILabelManager" />
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// The label repository
        /// </summary>
        private readonly ILabelRepository LabelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// </summary>
        /// <param name="LabelRepository">The label repository.</param>
        public LabelManager(ILabelRepository LabelRepository)
        {
            this.LabelRepository = LabelRepository;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<LabelModel> CreateLabel(LabelModel label)
        {
            try
            {
                return await this.LabelRepository.CreateLabel(label);
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
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<LabelModel> EditLabel(string labelID, string label)
        {
            try
            {
                return await this.LabelRepository.EditLabel(labelID, label);
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
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<LabelModel> AddLabel(LabelModel label)
        {
            try
            {
                return await this.LabelRepository.AddLabel(label);
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
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<bool> DeleteLabel(string labelID)
        {
            try
            {
                return await this.LabelRepository.DeleteLabel(labelID);
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
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<LabelModel> RemoveLabel(string labelID)
        {
            try
            {
                return await this.LabelRepository.RemoveLabel(labelID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<LabelModel> GetLabel(string userId)
        {
            try
            {
                return this.LabelRepository.GetLabel(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<LabelModel> GetLabelByNoteId(string noteId)
        {
            try
            {
                return this.LabelRepository.GetLabelByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the note by label identifier.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>Response from this API</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public IEnumerable<LabelModel> GetNoteByLabelId(string labelID)
        {
            try
            {
                return this.LabelRepository.GetNoteByLabelId(labelID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
