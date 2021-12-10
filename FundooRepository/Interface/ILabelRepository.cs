// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "ILabelRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    public interface ILabelRepository
    {
        Task<string> CreateLabel(LabelModel label);

        Task<string> EditLabel(string labelID, string label);

        Task<string> AddLabel(LabelModel label);

        Task<string> RemoveLabel(string labelID);

        Task<string> DeleteLabel(string labelID);

        IEnumerable<LabelModel> GetLabel();

        IEnumerable<LabelModel> GetLabelByNoteId(string noteId);

        IEnumerable<LabelModel> GetNoteByLabelId(string labelID);
    }
}
