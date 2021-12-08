﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "INoteRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        Task<string> AddNote(NoteModel note);
        Task<string> EditNote(NoteModel note);
        Task<string> AddReminder(string NoteID, string Reminder);
        Task<string> RemoveReminder(string NoteID);
        Task<string> PinnedUnPinned(string NoteID);
        Task<string> ArchiveUnArchive(string NoteID);
        Task<string> EditColor(string NoteID, string color);
        Task<string> ImageUpload(string noteID, IFormFile image);
        Task<string> Trash(string NoteID);
        Task<string> Restore(string NoteID);
        Task<string> DeleteForever(string NoteID);
        IEnumerable<NoteModel> GetNotes();
        IEnumerable<NoteModel> GetReminder();
        IEnumerable<NoteModel> GetArchive();
        IEnumerable<NoteModel> GetTrash();

    }
}
