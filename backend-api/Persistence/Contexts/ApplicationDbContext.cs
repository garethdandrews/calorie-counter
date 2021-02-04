using backend_api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}