using ApiCine.Model;

namespace ApiCine.Services
{
    public interface IPeliculaService
    {
        Task<IEnumerable<pelicula>> ObtenerTodas();
        Task<IEnumerable<pelicula>> BuscarPorNombre(string nombre);
        Task<IEnumerable<pelicula>> BuscarPorFecha(string fechaTexto);
        Task<string> VerificarDisponibilidadSala(string nombreSala);
        Task<pelicula> Crear(PeliculaDto dto);
        Task<bool> Actualizar(int id, PeliculaDto dto);
        Task<bool> Desactivar(int id);

        Task Asignar(AsignacionDto dto);
        Task<IEnumerable<object>> ObtenerAsignaciones();
    }
}
