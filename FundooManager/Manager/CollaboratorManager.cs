﻿using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorManager: ICollaboratorManager
    {
        private readonly ICollaboratorRepository CollaboratorRepository;
        public CollaboratorManager(ICollaboratorRepository CollaboratorRepository)
        {
            this.CollaboratorRepository = CollaboratorRepository;
        }

        public async Task<string> AddEmail(CollaboratorModel email)
        {
            try
            {
                return await this.CollaboratorRepository.AddEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteEmail(CollaboratorModel email)
        {
            try
            {
                return await this.CollaboratorRepository.DeleteEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CollaboratorModel> GetCollaborators(string noteId)
        {
            try
            {
                return this.CollaboratorRepository.GetCollaborators(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
