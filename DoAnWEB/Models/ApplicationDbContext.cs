using Microsoft.EntityFrameworkCore;

namespace DoAnWEB.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {

        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
