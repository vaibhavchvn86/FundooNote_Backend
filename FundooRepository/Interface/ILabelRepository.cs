using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        Task<string> CreateLabel(LabelModel label);
        Task<string> EditLabel(LabelModel label);
        Task<string> AddLabel(LabelModel label);
        Task<string> DeleteLabel(LabelModel label);
        Task<string> RemoveLabel(LabelModel label);
        IEnumerable<LabelModel> GetLabelByNoteId(string noteId);
        IEnumerable<LabelModel> GetLabelByUserId(string userId);
    }
}
