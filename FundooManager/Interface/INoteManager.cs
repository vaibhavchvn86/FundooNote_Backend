using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        string AddNote(NoteModel note);
        string EditTitle(NoteModel note);
        string EditDescription(NoteModel note);
        string EditReminder(NoteModel note);
        string EditPinned(NoteModel note);
        string EditArchive(NoteModel note);
        string EditColor(NoteModel note);
        string Trash(NoteModel note);
        string DeleteForever(NoteModel note);
    }
}
