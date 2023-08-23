using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepoLayer.Context;
using System.Collections.Generic;
using RepoLayer.Entity;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Fundoo_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBusiness notesBusiness;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public NotesController(INotesBusiness notesBusiness, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.notesBusiness = notesBusiness;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "notesList";
            string serializednotesList;
            var notesList = new List<NotesEntity>();
            var redisnotesList = await distributedCache.GetAsync(cacheKey);
            if (redisnotesList != null)
            {
                serializednotesList = Encoding.UTF8.GetString(redisnotesList);
                notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializednotesList);
            }
            else
            {
                notesList = await fundooContext.Notes.ToListAsync();
                serializednotesList = JsonConvert.SerializeObject(notesList);
                redisnotesList = Encoding.UTF8.GetBytes(serializednotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisnotesList, options);
            }
            return Ok(notesList);
        }



        [HttpGet]
        [Route("notes")]
        [Authorize]
        public IActionResult GetAllNotes()
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.GetAllNotes(userId);
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
        [HttpPost]
        [Route("archive")]
        [Authorize]
        public IActionResult Archive(int notesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.Archive(notesId, userId);
            if (result)
            {
                return Ok(new { succes = true, message = "Notes Archived", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Notes Unarchived", data = result });
            }
        }
        [HttpPost]
        [Route("pin")]
        [Authorize]
        public IActionResult Pin(int notesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.Pin(notesId, userId);
            if (result)
            {
                return Ok(new { succes = true, message = "Notes Pinned", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Notes Unpinned", data = result });
            }
        }
        [HttpPost]
        [Route("trash")]
        [Authorize]
        public IActionResult Trash(int notesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.Trash(notesId, userId);
            if (result)
            {
                return Ok(new { succes = true, message = "Notes Trashed", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Notes Not Trashed", data = result });
            }
        }
        [HttpPost]
        [Route("color")]
        [Authorize]
        public IActionResult Color(int notesId, string color)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBusiness.Color(notesId, userId, color);
            if (result != null)
            {
                return Ok(new { succes = true, message = $"Color is {color}", data = result });
            }
            else
            {
                return BadRequest(new { succes = false, message = "Something went wrong...", data = result });
            }
        }
        [HttpPost]
        [Route("uploadImage")]
        [Authorize]
        public async Task<IActionResult> AddImage(int notesId, IFormFile imageFile)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                if (userId != null)
                {
                    var result = await notesBusiness.AddImage(notesId, userId, imageFile);
                    if (result.Item1 == 1)
                    {
                        return Ok(new { success = true, message = "Image uploaded Successfully" });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = result.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, messaage = "Error Occurred : " + ex.Message });
            }
            return null;
        }
    }
}
