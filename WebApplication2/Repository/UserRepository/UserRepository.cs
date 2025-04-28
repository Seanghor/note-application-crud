using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication2.Entities;

namespace WebApplication2.Repository.UserRepository
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
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }



        public async Task AddAsync(UserEntity user)
        {
            using var connection = GetConnection();
            var query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT CAST(SCOPE_IDENTITY() as int);";
            int newId = await connection.QuerySingleAsync<int>(query, user);
            user.Id = newId;
        }


        public async Task<List<UserEntity>> GetAllAsync()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Users";
                var users = await connection.QueryAsync<UserEntity>(query);
                return users.ToList();
            }
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Id = @Id";
                var user = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { Id = id });
                return user;
            }
        }
        public async Task<UserEntity> GetByUsernameAsync(string username)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Username = @Username";
                var user = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { Username = username });
                if (user == null)
                    return null;
                return user;
            }
        }

        Task<List<UserEntity>> IUserRepository.GetAllAsync()
        {
            return GetAllAsync();
        }

    }
}
