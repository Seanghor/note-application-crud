using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication2.Entities;

namespace WebApplication2.Repository.NoteRepository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IConfiguration _configuration;
        public NoteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }


        /// ---- Logic
        public async Task AddAsync(NoteEntity note)
        {
            using var connection = GetConnection();
            //await connection
            //    .ExecuteAsync("INSERT INTO NOTES (Title, Content, Created_At, Updated_At, UserId) VALUES (@Title, @Content, @Created_At, @Updated_At, @UserId)", note);
            var query = "INSERT INTO NOTES (Title, Content, Created_At, Updated_At, UserId) VALUES (@Title, @Content, @Created_At, @Updated_At, @UserId); SELECT CAST(SCOPE_IDENTITY() as int);";
            int newId = await connection.QuerySingleAsync<int>(query, note);   // query get Id
            note.Id = newId;
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            var query = "DELETE FROM NOTES WHERE Id = @Id";
            await connection
                .ExecuteAsync(query, new { Id = id });
        }

        public async Task DeleteAllAsync(int userId)
        {
            using var connection = GetConnection();
            var query = "DELETE FROM NOTES WHERE UserId = @UserId";
            await connection
                .ExecuteAsync(query, new { UserId = userId });
        }


        public async Task<List<NoteEntity>> GetAllAsync(int userId, string? search = null, string? sortBy = "Created_At", bool descending = false, string? titleFilter = null)
        {
            using var connection = GetConnection();
            var query = "SELECT * FROM NOTES WHERE UserId = @UserId";

            // -- Add filters
            if (!string.IsNullOrEmpty(search))
                query += " AND (Title LIKE @Search OR Content LIKE @Search)";
            // -- Add title filter
            if (!string.IsNullOrEmpty(titleFilter))
                query += " AND Title LIKE @TitleFilter";

            // -- Add sorting
            if (sortBy == "Created_At")
                query += descending ? " ORDER BY Created_At DESC" : " ORDER BY Created_At ASC";
            else if (sortBy == "Title")
                query += descending ? " ORDER BY Title DESC" : " ORDER BY Title ASC";
            var notes = await connection.QueryAsync<NoteEntity>(query, new
            {
                UserId = userId,
                Search = $"%{search}%",
                TitleFilter = $"%{titleFilter}%"
            });
            return notes.ToList();
        }

        public async Task<NoteEntity> GetByIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var getNote = await connection
                    .QueryFirstOrDefaultAsync<NoteEntity>("SELECT * FROM NOTES WHERE ID = @Id", new { Id = id });
                return getNote;
            }
        }

        public Task<List<NoteEntity>> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(NoteEntity note)
        {
            using var connection = GetConnection();
            var query = @"UPDATE NOTES SET 
                  Title = @Title, 
                  Content = @Content, 
                  Updated_At = @Updated_At
                  WHERE Id = @Id";

            await connection.ExecuteAsync(query, note);
            //await connection
            //    .ExecuteAsync("UPDATE NOTES SET Title = @Title, Content = @Content, Created_At = @Created_At, Updated_At = @Updated_At WHERE Id = @Id", note);
        }

    }

}
