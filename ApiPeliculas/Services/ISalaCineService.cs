using ApiCine.Model;

namespace ApiCine.Services
{
    public interface ISalaCineService
    {
        Task<IEnumerable<sala_cine>> ObtenerTodas();
        Task<sala_cine?> ObtenerPorId(int id);
        Task<sala_cine> Crear(SalaCineDto dto);
        Task<bool> Actualizar(int id, SalaCineDto dto);
        Task<bool> Desactivar(int id);
    }
}
