using ApiCine.Model;
using ApiCine.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaService _service;

        public PeliculaController(IPeliculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<pelicula>>> Get()
            => Ok(await _service.ObtenerTodas());

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNombre(string nombre)
            => Ok(await _service.BuscarPorNombre(nombre));

        [HttpGet("por-fecha")]
        public async Task<IActionResult> BuscarPorFecha(string fecha)
        {
            try
            {
                var resultado = await _service.BuscarPorFecha(fecha);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // error fecha inválida
            }
        }

        [HttpGet("disponibilidad-sala")]
        public async Task<IActionResult> VerificarSala(string nombreSala)
        {
            var mensaje = await _service.VerificarDisponibilidadSala(nombreSala);
            return Ok(mensaje);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PeliculaDto dto)
        {
            var nueva = await _service.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = nueva.id_pelicula }, nueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, PeliculaDto dto)
        {
            var exito = await _service.Actualizar(id, dto);
            if (!exito) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var exito = await _service.Desactivar(id);
            if (!exito) return NotFound();
            return NoContent();
        }


        //Asignación de películas a salas de cine 
        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar(AsignacionDto dto)
        {
            await _service.Asignar(dto);
            return Ok("Asignación creada correctamente");
        }

        [HttpGet("asignaciones")]
        public async Task<IActionResult> GetAsignaciones()
            => Ok(await _service.ObtenerAsignaciones());
    }
}
