// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "NoteManager.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        public async Task<string> AddReminder(string NoteID, string Reminder)
        {
            try
            {
                return await this.NoteRepository.AddReminder(NoteID, Reminder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> RemoveReminder(string NoteID)
        {
            try
            {
                return await this.NoteRepository.RemoveReminder(NoteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> PinnedUnPinned(string NoteID)
        {
            try
            {
                return await this.NoteRepository.PinnedUnPinned(NoteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> ArchiveUnArchive(string NoteID)
        {
            try
            {
                return await this.NoteRepository.ArchiveUnArchive(NoteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> EditColor(string NoteID, string color)
        {
            try
            {
                return await this.NoteRepository.EditColor(NoteID, color);
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
        public async Task<string> Trash(string noteID)
        {
            try
            {
                return await this.NoteRepository.Trash(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Restore(string noteID)
        {
            try
            {
                return await this.NoteRepository.Restore(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteForever(string noteID)
        {
            try
            {
                return await this.NoteRepository.DeleteForever(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetNotes()
        {
            try
            {
                return this.NoteRepository.GetNotes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetArchive()
        {
            try
            {
                return this.NoteRepository.GetArchive();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetTrash()
        {
            try
            {
                return this.NoteRepository.GetTrash();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetReminder()
        {
            try
            {
                return this.NoteRepository.GetReminder();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
