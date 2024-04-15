using DoAnWEB.Models;

namespace DoAnWEB.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task AddAsync(Genre genres);
        Task UpdateAsync(Genre genres);
        Task DeleteAsync(int id);
    }
}
