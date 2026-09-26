using ApiCine.Model;

namespace ApiCine.Repository
{
    public interface IPeliculaRepository
    {
        Task<IEnumerable<pelicula>> GetAllAsync();
        Task<pelicula?> GetByIdAsync(int id);
        Task<IEnumerable<pelicula>> GetByNombreAsync(string nombre);
        Task<IEnumerable<pelicula>> GetByFechaPublicacionAsync(DateOnly fecha);
        Task AddAsync(pelicula pelicula);
        Task UpdateAsync(pelicula pelicula);
        Task<bool> DeactivateAsync(int id);
        Task<bool> SalaExisteAsync(string nombreSala);
        Task<int> ContarPeliculasPorSalaAsync(string nombreSala);

        Task AsignarAsync(pelicula_salacine asignacion);
        Task<IEnumerable<object>> GetAsignacionesAsync();
    }
}
