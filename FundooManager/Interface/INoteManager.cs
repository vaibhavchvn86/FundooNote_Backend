using FundooModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> AddNote(NoteModel note);
        Task<string> EditNote(NoteModel note);
        Task<string> AddReminder(NoteModel note);
        Task<string> RemoveReminder(NoteModel note);
        Task<string> PinnedUnPinned(NoteModel note);
        Task<string> ArchiveUnArchive(NoteModel note);
        Task<string> EditColor(NoteModel note);
        Task<string> ImageUpload(string noteID, IFormFile image);
        Task<string> Trash(NoteModel note);
        Task<string> Restore(NoteModel note);
        Task<string> DeleteForever(NoteModel note);
        IEnumerable<NoteModel> GetNotes(string userId);
        IEnumerable<NoteModel> GetReminder(string userId);
        IEnumerable<NoteModel> GetArchive(string userId);
        IEnumerable<NoteModel> GetTrash(string userId);

    }
}
