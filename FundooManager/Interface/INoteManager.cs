using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> AddNote(NoteModel note);
        Task<string> EditTitle(NoteModel note);
        Task<string> EditDescription(NoteModel note);
        Task<string> EditReminder(NoteModel note);
        Task<string> EditPinned(NoteModel note);
        Task<string> EditArchive(NoteModel note);
        Task<string> EditColor(NoteModel note);
        Task<string> Trash(NoteModel note);
        //Task<string> Restore(NoteModel note);
        Task<string> DeleteForever(NoteModel note);
    }
}
