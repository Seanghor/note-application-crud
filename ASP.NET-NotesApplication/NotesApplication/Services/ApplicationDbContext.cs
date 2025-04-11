using Microsoft.EntityFrameworkCore;
using NotesApplication.Entities;
using NotesApplication.Model;

namespace NotesApplication.Services
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Seeding user
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(

                  new User
                  {
                      Id = 1,
                      Username = "seanghor",
                      Password = "password"
                  },
                new User
                {
                    Id = 2,
                    Username = "techBodia",
                    Password = "password"
                }
                );
        }
    }
}
