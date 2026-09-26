using ApiCine.Model;
using ApiCine.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCine.Controllers
    
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaCineController : ControllerBase
{
    private readonly ISalaCineService _service;

    public SalaCineController(ISalaCineService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.ObtenerTodas());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var sala = await _service.ObtenerPorId(id);
        if (sala == null) return NotFound();
        return Ok(sala);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(SalaCineDto dto)
    {
        var nueva = await _service.Crear(dto);
        return CreatedAtAction(nameof(GetPorId), new { id = nueva.id_sala }, nueva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, SalaCineDto dto)
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
}
}
