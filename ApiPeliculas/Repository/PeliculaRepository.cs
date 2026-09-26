using ApiCine.Contexts;
using ApiCine.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiCine.Repository
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly CineContext _context;

        public PeliculaRepository(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<pelicula>> GetAllAsync()
            => await _context.peliculas.Where(p => p.activo == true).ToListAsync();

        public async Task<pelicula?> GetByIdAsync(int id)
            => await _context.peliculas.FindAsync(id);

        public async Task<IEnumerable<pelicula>> GetByNombreAsync(string nombre)
     => await _context.peliculas.Where(p => p.nombre.Contains(nombre) && p.activo == true).ToListAsync();

        public async Task<IEnumerable<pelicula>> GetByFechaPublicacionAsync(DateOnly fecha)
        {
            return await _context.pelicula_salacines
                .Where(ps => ps.fecha_publicacion == fecha)
                .Select(ps => ps.id_peliculaNavigation)
                .Distinct()
                .ToListAsync();
        }

        public async Task AddAsync(pelicula pelicula)
        {
            _context.peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(pelicula pelicula)
        {
            _context.Entry(pelicula).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var p = await _context.peliculas.FindAsync(id);
            if (p == null) return false;
            p.activo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SalaExisteAsync(string nombreSala)
             => await _context.sala_cines.AnyAsync(s => s.nombre == nombreSala);

        public async Task<int> ContarPeliculasPorSalaAsync(string nombreSala)
        {
            var resultado = await _context.Database
                .SqlQueryRaw<int>("EXEC SP_ContarPeliculasPorSala @p0", nombreSala)
                .ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task AsignarAsync(pelicula_salacine asignacion)
        {
            _context.pelicula_salacines.Add(asignacion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAsignacionesAsync()
        {
            return await _context.pelicula_salacines
                .Select(ps => new
                {
                    ps.id_pelicula_sala,
                    ps.fecha_publicacion,
                    ps.fecha_fin,
                    pelicula = ps.id_peliculaNavigation.nombre,
                    sala = ps.id_sala_cineNavigation.nombre
                })
                .ToListAsync();

        }
    }
}