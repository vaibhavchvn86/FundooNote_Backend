// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "INoteRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company = "BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;
    using Microsoft.AspNetCore.Http;

    public interface INoteRepository
    {
        Task<NoteModel> AddNote(NoteModel note);

        Task<NoteModel> EditNote(NoteModel note);

        Task<NoteModel> AddReminder(string noteID, string reminder);

        Task<NoteModel> RemoveReminder(string noteID);

        Task<string> PinnedUnPinned(string noteID);

        Task<string> ArchiveUnArchive(string noteID);

        Task<NoteModel> EditColor(NoteModel color);

        Task<NoteModel> ImageUpload(string noteID, IFormFile image);

        Task<NoteModel> Trash(string noteID);

        Task<NoteModel> Restore(string noteID);

        Task<bool> DeleteForever(string noteID);

        IEnumerable<NoteModel> GetNotes(string userId);

        IEnumerable<NoteModel> GetReminder(string userId);

        IEnumerable<NoteModel> GetArchive(string userId);

        IEnumerable<NoteModel> GetTrash(string userId);
    }
}
