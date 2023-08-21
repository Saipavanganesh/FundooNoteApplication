using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Fundoo_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBusiness notesBusiness;
        public NotesController(INotesBusiness notesBusiness)
        {
            this.notesBusiness = notesBusiness;
        }
        [HttpPost]
        [Route("notes")]
        [Authorize]
        public IActionResult CreateNotes(CreateNotesModel notesModel)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.CreateNotes(notesModel, userId);
            if (result != null)
            {
                return Ok(new { succes = true, message = "Notes Created Successfully", data = result });
            }
            else
            {
                return BadRequest(new { succes = true, message = "Notes Created Successfully", data = result });
            }
        }
        [HttpGet]
        [Route("notes")]
        [Authorize]
        public IActionResult GetAllNotes()
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.GetAllNotes();
            if (result != null)
            {
                return Ok(new { succes = true, message = "Displaying Notes", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Something went wrong", data = result });
            }
        }
        [HttpGet]
        [Route("notesById")]
        [Authorize]
        public IActionResult GetNotesById(int NotesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.GetNotesById(NotesId);
            if (result != null)
            {
                return Ok(new { succes = true, message = "Displaying Notes", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Something went wrong", data = result });
            }
        }

        [HttpDelete]
        [Route("notes")]
        [Authorize]
        public IActionResult DeleteNotes(int NotesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.DeleteNotes(NotesId);
            if (result != null)
            {
                return Ok(new { succes = true, message = "Deleted Notes", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Something went wrong", data = result });
            }
        }
        [HttpPut]
        [Route("notes")]
        [Authorize]
        public IActionResult UpdateNotes(int notesId, CreateNotesModel model)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.UpdateNotes(notesId, model);
            if (result != null)
            {
                return Ok(new { succes = true, message = "Updated Notes Successfully", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Something went wrong", data = result });
            }
        }
    }
}
