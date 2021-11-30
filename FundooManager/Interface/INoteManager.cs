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
    }
}
