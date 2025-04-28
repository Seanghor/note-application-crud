using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Service
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<NoteEntity> Notes => Set<NoteEntity>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hashedPassword = "AQAAAAIAAYagAAAAEOj2eBQuQTIj658TT9UZHtPLV6cUw3kfiD8rTBHs2FcicnTfYavWTldRn+HJ7wsZbQ==";

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    Username = "TechBodia",
                    //Password = "password"
                    Password = hashedPassword
                },
                new UserEntity
                {
                    Id = 2,
                    Username = "user",
                    //Password = "password"
                    Password = hashedPassword
                }
            );
        }

    }
}
