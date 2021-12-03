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
        Task<string> ImageUpload(IFormFile image, string noteID);
        Task<string> Trash(NoteModel note);
        Task<string> Restore(NoteModel note);
        Task<string> DeleteForever(NoteModel note);
        IEnumerable<NoteModel> GetNotes(string userId);
    }
}
