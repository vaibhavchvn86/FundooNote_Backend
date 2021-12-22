// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "ILabelManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------


namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    public interface ILabelManager
    {
        Task<LabelModel> CreateLabel(LabelModel label);

        Task<LabelModel> EditLabel(string labelID, string label);

        Task<LabelModel> AddLabel(LabelModel label);

        Task<LabelModel> RemoveLabel(string labelID);

        Task<bool> DeleteLabel(string labelID);

        IEnumerable<LabelModel> GetLabel(string userId);

        IEnumerable<LabelModel> GetLabelByNoteId(string noteId);

        IEnumerable<LabelModel> GetNoteByLabelId(string labelID);
    }
}
