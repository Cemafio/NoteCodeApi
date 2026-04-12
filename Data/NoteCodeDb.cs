using Microsoft.EntityFrameworkCore;
using NoteCodeApi.Models;

namespace NoteCodeApi.Data
{
    public class NoteCodeDb : DbContext
    {
        public NoteCodeDb(DbContextOptions<NoteCodeDb> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<NoteUser> NoteUsers { get; set; }
    }
}