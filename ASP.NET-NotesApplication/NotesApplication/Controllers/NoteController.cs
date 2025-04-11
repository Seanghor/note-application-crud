using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApplication.Entities;
using NotesApplication.Model;
using NotesApplication.Repositories.Notes;
using NotesApplication.Services;

namespace NotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // === Helper method
        private int GetUserId()
        {
            return 1;
        }

        //-- Get All
        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetAll(
            string? search,
            string? sortBy = "Created_At",
            bool descending = false,
            string? titleFilter = null
            )
        {
            var userId = GetUserId();
            var notes = await _noteRepository.GetAllAsync(userId, search, sortBy, descending, titleFilter);
            return Ok(notes);
        }


        //-- Get One
        [HttpGet("{id}", Name = "GetOne")]
        public async Task<ActionResult<Note>> GetOne(int id)
        {
            var userId = GetUserId();
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
                return NotFound("This video note does not exist in the database.");
            if (note.UserId != userId)
                return Unauthorized("You are not authorize to perfomce.");
            return Ok(note);
        }


        //-- Create 
        [HttpPost]
        public async Task<ActionResult<Note>> Create(Note newNote)
        {
            var userId = GetUserId();
            if (newNote == null)
            {
                return BadRequest("Note cannot be null");
            }
            //newNote.Created_At = DateTime.Now;
            //newNote.Updated_At = DateTime.Now;
            newNote.UserId = userId;
            await _noteRepository.AddAsync(newNote);
            //return CreatedAtAction(nameof(GetOne), new {id = newNote.Id}, newNote);
            return CreatedAtAction("GetOne", new { id = newNote.Id }, newNote);
        }


        //-- Update 
        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> Update(int id, Note updateNote)
        {
            var userId = GetUserId();
            if (updateNote == null)
                return BadRequest("Note cannot be null");
            
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote is null)
                return NotFound("Note does not exist in the database.");

            if (existingNote.UserId != userId)
                return Unauthorized("You can only update your own notes.");

            existingNote.Title = updateNote.Title; 
            existingNote.Content = updateNote.Content;
            existingNote.Updated_At = DateTime.Now;
            await _noteRepository.UpdateAsync(existingNote);
            return NoContent();
        }


        //-- Delete 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> Delete(int id)
        {
            var userId = GetUserId();
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote is null)
                return NotFound("Note does not exist in the database.");

            if (existingNote.UserId != userId)
                return Unauthorized("You can only delete your own notes.");

            await _noteRepository.DeleteAsync(id);
            return NoContent();
        }

        //-- Delete All
        [HttpDelete("deleteAll")]
        public async Task<ActionResult<Note>> DeleteAll()
        {
            var userId = GetUserId();
            await _noteRepository.DeleteAllAsync(userId);
            return NoContent();
        }

    }
}
