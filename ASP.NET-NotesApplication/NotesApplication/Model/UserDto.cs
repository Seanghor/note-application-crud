namespace NotesApplication.Model
{

    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CredentailResponseDto
    {
        public string UserId { get; set; } = string.Empty;
    }
}
