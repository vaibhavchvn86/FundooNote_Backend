// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "ILabelManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        Task<string> CreateLabel(LabelModel label);
        Task<string> EditLabel(string LabelID, string label);
        Task<string> AddLabel(LabelModel label);
        Task<string> RemoveLabel(string LabelID);
        Task<string> DeleteLabel(string LabelID);
        IEnumerable<LabelModel> GetLabel();
        IEnumerable<LabelModel> GetLabelByNoteId(string NoteId);
        IEnumerable<LabelModel> GetNoteByLabelId(string LabelID);
    }
}
