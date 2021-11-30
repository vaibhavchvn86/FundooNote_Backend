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
        public string EditDescription(NoteModel note)
        {
            try
            {
                return this.NoteRepository.EditDescription(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditReminder(NoteModel note)
        {
            try
            {
                return this.NoteRepository.EditReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditPinned(NoteModel note)
        {
            try
            {
                return this.NoteRepository.EditPinned(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditArchive(NoteModel note)
        {
            try
            {
                return this.NoteRepository.EditArchive(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EditColor(NoteModel note)
        {
            try
            {
                return this.NoteRepository.EditColor(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
