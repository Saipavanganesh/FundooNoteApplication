using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooContext fundooContext;
        public NotesRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NotesEntity CreateNotes(CreateNotesModel notesModel, long userId)
        {
            try
            {
                NotesEntity notes = new NotesEntity();
                notes.Title = notesModel.Title;
                notes.Description = notesModel.Description;
                notes.UserId = userId;
                fundooContext.Notes.Add(notes);
                fundooContext.SaveChanges();
                if (notes != null)
                {
                    return notes;
                }
                return null;
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
                List<NotesEntity> notesList = new List<NotesEntity>();
                notesList = fundooContext.Notes.ToList();
                return notesList;
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
                var notesEntity = fundooContext.Notes.FirstOrDefault(n => n.NotesId == notesId);
                if (notesId == notesEntity.NotesId)
                {
                    List<NotesEntity> NotesList = new List<NotesEntity>();
                    NotesList.Add(notesEntity);
                    return NotesList;
                }
                return null;
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
                var deletedNotes = fundooContext.Notes.FirstOrDefault(n => n.NotesId == notesId);
                if (notesId == deletedNotes.NotesId)
                {
                    fundooContext.Notes.Remove(deletedNotes);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var uNotes = fundooContext.Notes.FirstOrDefault(u => u.NotesId == notesId);
                if (uNotes != null)
                {
                    uNotes.Title = model.Title;
                    uNotes.Description = uNotes.Description + " " + model.Description;
                    fundooContext.Notes.Update(uNotes);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
