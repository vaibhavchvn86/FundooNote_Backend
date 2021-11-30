using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        string AddNote(NoteModel note);
        string EditNote(NoteModel note);
        string EditDescription(NoteModel note);
        //string EditRemainder(NoteModel note);
        //string EditPinned(NoteModel note);
        //string EditArchive(NoteModel note);
        //string EditColor(NoteModel note);
    }
}
