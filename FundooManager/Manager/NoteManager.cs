using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository NoteRepository;
        public NoteManager(INoteRepository NoteRepository)
        {
            this.NoteRepository = NoteRepository;
        }

        public string AddNote(NoteModel note)
        {
            try
            {
                return this.NoteRepository.AddNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditNote(NoteModel note)
        {
            try
            {
                return this.NoteRepository.EditNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
