using ApiCine.Model;
using ApiCine.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiCine.Services
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _repo;

        public PeliculaService(IPeliculaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<pelicula>> ObtenerTodas()
            => await _repo.GetAllAsync();

        // Busca películas por nombre
        public async Task<IEnumerable<pelicula>> BuscarPorNombre(string nombre)
            => await _repo.GetByNombreAsync(nombre);

        // Busca películas por fecha de publicación, validando el formato de la fecha
        public async Task<IEnumerable<pelicula>> BuscarPorFecha(string fechaTexto)
        {
            if (!DateOnly.TryParse(fechaTexto, out var fecha))
                throw new ArgumentException("La fecha proporcionada no es válida. Use el formato yyyy-MM-dd.");

            return await _repo.GetByFechaPublicacionAsync(fecha);
        }

        // Verifica la disponibilidad de una sala según la cantidad de películas asignadas
        public async Task<string> VerificarDisponibilidadSala(string nombreSala)
        {
            bool existe = await _repo.SalaExisteAsync(nombreSala);
            if (!existe)
                return "Sala no encontrada";

            int total = await _repo.ContarPeliculasPorSalaAsync(nombreSala);

            if (total < 3)
                return "Sala disponible";
            else if (total >= 3 && total <= 5)
                return $"Sala con {total} películas asignadas";
            else
                return "Sala no disponible";
        }

        // Crea una nueva película
        public async Task<pelicula> Crear(PeliculaDto dto)
        {
            var nueva = new pelicula
            {
                nombre = dto.nombre,
                duracion = dto.duracion,
                activo = true
            };
            await _repo.AddAsync(nueva);
            return nueva;
        }

        // Actualiza una película existente
        public async Task<bool> Actualizar(int id, PeliculaDto dto)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente == null) return false;

            existente.nombre = dto.nombre;
            existente.duracion = dto.duracion;

            await _repo.UpdateAsync(existente);
            return true;
        }

        // Desactiva una película por su ID
        public async Task<bool> Desactivar(int id)
            => await _repo.DeactivateAsync(id);

        public async Task Asignar(AsignacionDto dto)
        {
            var asignacion = new pelicula_salacine
            {
                id_pelicula = dto.id_pelicula,
                id_sala_cine = dto.id_sala_cine,
                fecha_publicacion = dto.fecha_publicacion,
                fecha_fin = dto.fecha_fin
            };
            await _repo.AsignarAsync(asignacion);
        }

        public async Task<IEnumerable<object>> ObtenerAsignaciones()
    => await _repo.GetAsignacionesAsync();
    }
}
