using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private readonly INotesRepo notesRepo;
        public NotesBusiness(INotesRepo notesRepo)
        {
            this.notesRepo = notesRepo;
        }
        public NotesEntity CreateNotes(CreateNotesModel notesModel, long userId)
        {
            try
            {
                return notesRepo.CreateNotes(notesModel, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotesEntity> GetAllNotes(long userId)
        {
            try
            {
                return notesRepo.GetAllNotes(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotesEntity> GetNotesById(int notesId)
        {
            try
            {
                return notesRepo.GetNotesById(notesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteNotes(int notesId)
        {
            try
            {
                return notesRepo.DeleteNotes(notesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateNotes(int notesId, CreateNotesModel model)
        {
            try
            {
                return notesRepo.UpdateNotes(notesId, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Archive(int notesId, long userId)
        {
            try
            {
                return notesRepo.Archive(notesId,userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Pin(int notesId, long userId)
        {
            try
            {
                return notesRepo.Pin(notesId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Trash(int notesId, long userId)
        {
            try
            {
                return notesRepo.Trash(notesId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Color(int notesId, long userId, string color)
        {
            try
            {
                return notesRepo.Color(notesId, userId, color);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public async Task<Tuple<int, string>> AddImage(int notesId, long userId, IFormFile imageFile)
        {
            try
            {
                return await notesRepo.AddImage(notesId, userId, imageFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
