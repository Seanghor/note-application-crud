using NotesApplication.Entities;
using NotesApplication.Model;

namespace NotesApplication.Repositories.Notes
{
    public interface INoteRepository
    {
        // -- Get All
        Task<List<Note>> GetAllAsync(int userId, string? search = null, string? sortBy = "Created_At", bool descending = false, string? titleFilter = null);
        // -- Get by Id
        Task<Note> GetByIdAsync(int id);
        // -- Get by UserId
        Task<List<Note>> GetByUserIdAsync(int userId);
        // -- Create
        Task AddAsync(Note note);
        // -- Update
        Task UpdateAsync(Note note);
        // -- Delete
        Task DeleteAsync(int id);

        Task DeleteAllAsync(int userId);
    }
}
