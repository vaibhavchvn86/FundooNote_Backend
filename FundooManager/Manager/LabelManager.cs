using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

        public async Task<string> EditLabel(LabelModel label)
        {
            try
            {
                return await this.LabelRepository.EditLabel(label);
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
        public async Task<string> DeleteLabel(LabelModel label)
        {
            try
            {
                return await this.LabelRepository.DeleteLabel(label);
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
                return await this.LabelRepository.RemoveLabel(label);
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
                return this.LabelRepository.GetLabelByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelModel> GetLabelByUserId(string userId)
        {
            try
            {
                return this.LabelRepository.GetLabelByUserId(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
