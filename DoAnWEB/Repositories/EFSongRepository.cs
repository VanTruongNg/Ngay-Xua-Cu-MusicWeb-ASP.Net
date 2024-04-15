using DoAnWEB.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEB.Repositories
{
    public class EFSongRepository : ISongRepository
    {
        private readonly ApplicationDbContext _context;

        public EFSongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            return await _context.Songs.Include(g => g.Genre).ToListAsync();
        }

        public async Task<Song> GetByIdAsync(int id)
        {
            return await _context.Songs.Include(x => x.Genre).SingleOrDefaultAsync(x=>x.Id == id);
        }

        public async Task AddAsync(Song songs)
        {
            _context.Songs.Add(songs);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Song songs)
        {
            _context.Update(songs);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Song>> SearchAsync(string query)
        {
            return await _context.Songs
                .Where(s => s.Name.Contains(query) || s.Artist.Contains(query))
                .Include(g => g.Genre)
                .ToListAsync();
        }
    }
}
