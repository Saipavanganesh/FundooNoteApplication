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
        public List<NotesEntity> GetAllNotes(long userId);
        public List<NotesEntity> GetNotesById(int notesId);
        public bool DeleteNotes(int notesId);
        public bool UpdateNotes(int notesId, CreateNotesModel model);
        public bool Archive(int notesId, long userId);
        public bool Pin(int notesId, long userId);
        public bool Trash(int notesId, long userId);
        public string Color(int notesId, long userId, string color);
        public Task<Tuple<int, string>> AddImage(int notesId, long userId, IFormFile imageFile);
    }
}
