﻿namespace NotesApplication.Entities

{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public  string Password { get; set; }
        public List<Note>? Notes { get; set; }
    }

    //public class UserDto
    //{
    //    public  string Username { get; set; } = string.Empty;
    //    public  string Password { get; set; } = string.Empty;
    //}
}
