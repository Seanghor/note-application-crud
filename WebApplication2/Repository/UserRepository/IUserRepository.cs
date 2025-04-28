using WebApplication2.Entities;

namespace WebApplication2.Repository.UserRepository
{
    public interface IUserRepository
    {
        // -- Get All
        Task<List<UserEntity>> GetAllAsync();
        // -- Get by Id
        Task<UserEntity> GetByIdAsync(int id);

        // -- Create
        Task AddAsync(UserEntity user);

        // -- Delete
        //Task DeleteAsync(int id);

        // -- find by username
        Task<UserEntity> GetByUsernameAsync(string username);
    }
}
