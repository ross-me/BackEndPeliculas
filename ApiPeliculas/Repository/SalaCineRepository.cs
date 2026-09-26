using ApiCine.Contexts;
using ApiCine.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiCine.Repository
{
    public class SalaCineRepository : ISalaCineRepository
    {
        private readonly CineContext _context;

        public SalaCineRepository(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<sala_cine>> GetAllAsync()
            => await _context.sala_cines.Where(s => s.estado == true).ToListAsync();

        public async Task<sala_cine?> GetByIdAsync(int id)
            => await _context.sala_cines.FindAsync(id);

        public async Task AddAsync(sala_cine sala)
        {
            _context.sala_cines.Add(sala);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(sala_cine sala)
        {
            _context.Entry(sala).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var s = await _context.sala_cines.FindAsync(id);
            if (s == null) return false;
            s.estado = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
