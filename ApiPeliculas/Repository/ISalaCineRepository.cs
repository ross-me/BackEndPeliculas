using ApiCine.Model;

namespace ApiCine.Repository
{
    public interface ISalaCineRepository
    {
        Task<IEnumerable<sala_cine>> GetAllAsync();
        Task<sala_cine?> GetByIdAsync(int id);
        Task AddAsync(sala_cine sala);
        Task UpdateAsync(sala_cine sala);
        Task<bool> DeactivateAsync(int id);
    }
}
