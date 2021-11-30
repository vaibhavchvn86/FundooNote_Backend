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
    }
}
