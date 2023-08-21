using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        private readonly FileService _fileService;
        private readonly Cloudinary _cloudinary;
        public NotesRepo(FundooContext fundooContext, IConfiguration configuration, FileService fileService, Cloudinary cloudinary)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
            this._cloudinary = cloudinary;
            this._fileService = fileService;
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
        public bool Archive(int notesId)
        {
            try
            {
                var notes = fundooContext.Notes.FirstOrDefault(u => u.NotesId == notesId);
                if (notes != null)
                {
                    notes.IsArchive = true;
                    fundooContext.Notes.Update(notes);
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

        public bool Pin(int notesId)
        {
            try
            {
                var notes = fundooContext.Notes.FirstOrDefault(u => u.NotesId == notesId);
                if (notes != null)
                {
                    notes.IsArchive = true;
                    fundooContext.Notes.Update(notes);
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

        public bool Trash(int notesId)
        {
            try
            {
                var notes = fundooContext.Notes.FirstOrDefault(u => u.NotesId == notesId);
                if (notes != null)
                {
                    notes.IsArchive = true;
                    fundooContext.Notes.Update(notes);
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
        public string Color(int notesId, string color)
        {
            try
            {
                var notes = fundooContext.Notes.FirstOrDefault(u => u.NotesId == notesId);
                if (notes != null)
                {
                    return color;
                }
                return null;
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
                var note = fundooContext.Notes.FirstOrDefault(data => data.NotesId == notesId && data.UserId == userId);
                if (note != null)
                {
                    var fileServiceResult = await _fileService.SaveImage(imageFile);
                    if (fileServiceResult.Item1 == 0)
                    {
                        return new Tuple<int, string>(0, fileServiceResult.Item2);
                    }
                    //Uploading image to Cloudinary
                    var uploading = new ImageUploadParams
                    {
                        File = new CloudinaryDotNet.FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                    };
                    ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploading);

                    //Update product entity with image url from cloudinary
                    string ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
                    note.ImagePath = ImageUrl;
                    fundooContext.Notes.Update(note);
                    fundooContext.SaveChanges();
                    return new Tuple<int, string>(1, "Product added with image succesfully");
                }
                return null;
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "An error occurred : " + ex.Message);
            }
        }
    }
}
