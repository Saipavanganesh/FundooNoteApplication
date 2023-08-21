using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interfaces
{
    public interface INotesRepo
    {
        public NotesEntity CreateNotes(CreateNotesModel notesModel, long userId);
        public List<NotesEntity> GetAllNotes();
        public List<NotesEntity> GetNotesById(int notesId);
        public bool DeleteNotes(int notesId);
        public bool UpdateNotes(int notesId, CreateNotesModel model);
    }
}
