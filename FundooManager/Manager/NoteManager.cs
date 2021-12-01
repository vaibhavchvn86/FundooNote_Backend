using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository NoteRepository;
        public NoteManager(INoteRepository NoteRepository)
        {
            this.NoteRepository = NoteRepository;
        }

        public async Task<string> AddNote(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.AddNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditTitle(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditTitle(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditDescription(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditDescription(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditReminder(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditPinned(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditPinned(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditArchive(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditArchive(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditColor(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditColor(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditImage(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditImage(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Trash(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.Trash(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteForever(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.DeleteForever(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
