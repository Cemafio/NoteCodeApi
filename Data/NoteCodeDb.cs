using Microsoft.EntityFrameworkCore;
using NoteCodeApi.Models;
using NoteCodeApi.Interfaces;

namespace NoteCodeApi.Data
{
    public class NoteCodeDb : DbContext
    {
        public NoteCodeDb(DbContextOptions<NoteCodeDb> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<NoteUser> NoteUsers { get; set; }
        public DbSet<TaskUser> TaskUsers {get; set;}

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<ITrackable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    // entry.Entity.UpdatedAt = DateTime.UtcNow;
                }

            }

            return base.SaveChanges();
        }
    }
    
}