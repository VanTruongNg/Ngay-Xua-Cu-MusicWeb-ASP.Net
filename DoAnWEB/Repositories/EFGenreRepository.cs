using Microsoft.EntityFrameworkCore;
using DoAnWEB.Models;


namespace DoAnWEB.Repositories
{
    public class EFGenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public EFGenreRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == id);
        }

        public async Task AddAsync(Genre genres)
        {
            _context.Genres.Add(genres);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genres)
        {
            _context.Genres.Update(genres);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
