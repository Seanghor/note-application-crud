namespace NotesApplication.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }  
        public string? Content { get; set; } 
        public DateTime Created_At { get; set; } = DateTime.Now;  // Auto-generated on creation
        public DateTime? Updated_At { get; set; }

        // Foreign key to the User table
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
