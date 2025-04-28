namespace WebApplication2.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<NoteEntity>? Notes { get; set; }

    }
}
