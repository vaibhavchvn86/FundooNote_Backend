// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "INoteManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;
    using Microsoft.AspNetCore.Http;

    public interface INoteManager
    {
        Task<string> AddNote(NoteModel note);

        Task<string> EditNote(NoteModel note);

        Task<string> AddReminder(string noteID, string reminder);

        Task<string> RemoveReminder(string noteID);

        Task<string> PinnedUnPinned(string noteID);

        Task<string> ArchiveUnArchive(string noteID);

        Task<string> EditColor(string noteID, string color);

        Task<string> ImageUpload(string noteID, IFormFile image);

        Task<string> Trash(string noteID);

        Task<string> Restore(string noteID);

        Task<string> DeleteForever(string noteID);

        IEnumerable<NoteModel> GetNotes();

        IEnumerable<NoteModel> GetReminder();

        IEnumerable<NoteModel> GetArchive();

        IEnumerable<NoteModel> GetTrash();
    }
}
