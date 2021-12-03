using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
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
        public async Task<string> EditNote(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.EditNote(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> AddReminder(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.AddReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> RemoveReminder(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.RemoveReminder(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> PinnedUnPinned(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.PinnedUnPinned(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> ArchiveUnArchive(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.ArchiveUnArchive(note);
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
        public async Task<string> ImageUpload(string noteID, IFormFile image)
        {
            try
            {
                return await this.NoteRepository.ImageUpload(noteID, image);
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
        public async Task<string> Restore(NoteModel note)
        {
            try
            {
                return await this.NoteRepository.Restore(note);
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

        public IEnumerable<NoteModel> GetNotes(string userId)
        {
            try
            {
                return this.NoteRepository.GetNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
