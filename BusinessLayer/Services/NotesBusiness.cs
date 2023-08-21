using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                return notesRepo.GetAllNotes();
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
    }
}
