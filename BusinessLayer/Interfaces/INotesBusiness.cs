using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INotesBusiness
    {
        public NotesEntity CreateNotes(CreateNotesModel notesModel, long userId);
        public List<NotesEntity> GetAllNotes();
        public List<NotesEntity> GetNotesById(int notesId);
        public bool DeleteNotes(int notesId);
        public bool UpdateNotes(int notesId, CreateNotesModel model);
        public bool Archive(int notesId);
        public bool Pin(int notesId);
        public bool Trash(int notesId);
        public string Color(int notesId, string color);
        public Task<Tuple<int, string>> AddImage(int notesId, long userId, IFormFile imageFile);
    }
}
