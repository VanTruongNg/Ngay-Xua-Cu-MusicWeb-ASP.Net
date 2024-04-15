using DoAnWEB.Models;

namespace DoAnWEB.Repositories
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song> GetByIdAsync(int id);
        Task AddAsync(Song songs);
        Task UpdateAsync(Song songs);
        Task DeleteAsync(int id);
        Task<IEnumerable<Song>> SearchAsync(string searchString);
    }
}
