using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        Task<string> CreateLabel(LabelModel label);
        Task<string> EditLabel(LabelModel label);
        Task<string> AddLabel(LabelModel label);
        Task<string> RemoveLabel(LabelModel label);
        Task<string> DeleteLabel(LabelModel label);
        IEnumerable<LabelModel> GetLabelByNoteId(string noteId);
        IEnumerable<LabelModel> GetLabelByUserId(string userId);
    }
}
