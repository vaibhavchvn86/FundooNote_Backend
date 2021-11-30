using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        string AddNote(NoteModel note);
        string EditNote(NoteModel note);
        string EditDescription(NoteModel note);
        string EditReminder(NoteModel note);
        //string EditPinned(NoteModel note);
        //string EditArchive(NoteModel note);
        string EditColor(NoteModel note);
    }
}
