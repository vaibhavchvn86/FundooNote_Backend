// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "LabelManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager: ILabelManager
    {
        private readonly ILabelRepository LabelRepository;
        public LabelManager(ILabelRepository LabelRepository)
        {
            this.LabelRepository = LabelRepository;
        }

        public async Task<string> CreateLabel(LabelModel label)
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

        public async Task<string> EditLabel(string LabelID, string label)
        {
            try
            {
                return await this.LabelRepository.EditLabel(LabelID, label);
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
                return await this.LabelRepository.AddLabel(label);
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
                return await this.LabelRepository.DeleteLabel(LabelID);
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
                return await this.LabelRepository.RemoveLabel(LabelID);
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
                return this.LabelRepository.GetLabel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetLabelByNoteId(string NoteId)
        {
            try
            {
                return this.LabelRepository.GetLabelByNoteId(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetNoteByLabelId(string LabelID)
        {
            try
            {
                return this.LabelRepository.GetNoteByLabelId(LabelID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
