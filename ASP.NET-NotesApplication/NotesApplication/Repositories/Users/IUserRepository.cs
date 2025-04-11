using NotesApplication.Entities;
using NotesApplication.Model;

namespace NotesApplication.Repositories.Users
{
    public interface IUserRepository
    {
        // -- Get All
        Task<List<User>> GetAllAsync();
        // -- Get by Id
        Task<User> GetByIdAsync(int id);

        // -- Create
        Task AddAsync(User user);

        // -- Delete
        Task DeleteAsync(int id);

        // -- find by username
        Task<User> GetByUsernameAsync(string username);
    }
}
