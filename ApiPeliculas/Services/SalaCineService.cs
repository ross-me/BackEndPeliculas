using ApiCine.Model;
using ApiCine.Repository;

namespace ApiCine.Services
{
    public class SalaCineService : ISalaCineService
    {
        private readonly ISalaCineRepository _repo;

        public SalaCineService(ISalaCineRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<sala_cine>> ObtenerTodas()
            => await _repo.GetAllAsync();

        public async Task<sala_cine?> ObtenerPorId(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<sala_cine> Crear(SalaCineDto dto)
        {
            var nueva = new sala_cine
            {
                nombre = dto.nombre,
                estado = true
            };
            await _repo.AddAsync(nueva);
            return nueva;
        }

        public async Task<bool> Actualizar(int id, SalaCineDto dto)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente == null) return false;

            existente.nombre = dto.nombre;
            await _repo.UpdateAsync(existente);
            return true;
        }

        public async Task<bool> Desactivar(int id)
            => await _repo.DeactivateAsync(id);
    }

}
