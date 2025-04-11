using NotesApplication.Model;
using System.Data.SqlClient;
using Dapper;
using NotesApplication.Entities;

namespace NotesApplication.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));
        }
        public async Task AddAsync(User user)
        {
            using var connection = GetConnection();
            var query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT CAST(SCOPE_IDENTITY() as int);";
            int newId = await connection.QuerySingleAsync<int>(query, user);
            user.Id = newId;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Id = @Id";
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
                return user;
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Username = @Username";
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
                return user;
            }
        }

    }
}
