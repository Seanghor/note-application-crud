using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Repository.NoteRepository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
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


        // === Get All
        [HttpGet(Name = "GetAllNote")]
        public async Task<ActionResult<List<NoteEntity>>> GetAll(string? search, string? sortBy = "Created_At", bool descending = false, string? titleFilter = null)
        {
            var userId = GetUserId();
            var notes = await _noteRepository.GetAllAsync(userId, search, sortBy, descending, titleFilter);
            if (notes == null || notes.Count == 0)
                return Ok(new List<NoteEntity>());
            return Ok(notes);
        }


        // === Get by Id
        [HttpGet("{id}", Name = "GetOneNoteById")]
        public async Task<ActionResult<NoteEntity>> GetById(int id)
        {
            var existingNote = await _noteRepository.GetByIdAsync(id);
            var userId = GetUserId();
            if (existingNote == null)
                return NotFound("This note does not exist in the database.");

            if (existingNote.UserId != userId)
                return Unauthorized("You are not authorized to view this note.");
            return Ok(existingNote);
        }


        // === Create
        [HttpPost(Name = "Create")]
        public async Task<ActionResult<NoteEntity>> Create(NoteEntity note)
        {
            var userId = GetUserId();
            if (note == null)
                return BadRequest("Note cannot be null.");
            note.UserId = userId;
            await _noteRepository.AddAsync(note);
            return CreatedAtAction("GetOneNoteById", new { id = note.Id }, note);
        }

        // === Update
        [HttpPut("{id}", Name = "UpdateNote")]
        public async Task<ActionResult<NoteEntity>> Update(int id, NoteEntity updateNote)
        {
            var userId = GetUserId();
            if (updateNote == null)
                return BadRequest("Note cannot be null.");
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null)
                return NotFound("This note does not exist in the database.");

            if (existingNote.UserId != userId)
                return Unauthorized("You are not authorized to update this note.");


            existingNote.Title = updateNote.Title;
            existingNote.Content = updateNote.Content;
            existingNote.Updated_At = DateTime.Now;
            await _noteRepository.UpdateAsync(existingNote);
            return NoContent();
        }

        // == Delete One
        [HttpDelete("{id}", Name = "DeleteOneNote")]
        public async Task<ActionResult<NoteEntity>> DeleteOne(int id)
        {
            var userId = GetUserId();
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null)
                return NotFound("This note does not exist in the database.");
            if (existingNote.UserId != userId)
                return Unauthorized("You are not authorized to delete this note.");
            await _noteRepository.DeleteAsync(id);
            return NoContent();
        }

        // == Delete All
        [HttpDelete("deleteAll", Name = "DeleteAllNote")]
        public async Task<ActionResult<NoteEntity>> DeleteAll()
        {
            var userId = GetUserId();
            await _noteRepository.DeleteAllAsync(userId);
            return NoContent();
        }
    }
}
