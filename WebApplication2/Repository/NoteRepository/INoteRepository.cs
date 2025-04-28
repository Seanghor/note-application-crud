using WebApplication2.Entities;

namespace WebApplication2.Repository.NoteRepository
{
    public interface INoteRepository
    {
        // -- Get All
        Task<List<NoteEntity>> GetAllAsync(int userId, string? search = null, string? sortBy = "Created_At", bool descending = false, string? titleFilter = null);
        // -- Get by Id
        Task<NoteEntity> GetByIdAsync(int id);
        // -- Get by UserId
        Task<List<NoteEntity>> GetByUserIdAsync(int userId);
        // -- Create
        Task AddAsync(NoteEntity note);
        // -- Update
        Task UpdateAsync(NoteEntity note);
        // -- Delete
        Task DeleteAsync(int id);

        Task DeleteAllAsync(int userId);
    }
}
