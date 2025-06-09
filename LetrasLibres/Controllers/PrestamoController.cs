using LetrasLibres.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetrasLibres.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LetrasLibres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly BibliotecaDbContext dbcontext;

        public PrestamoController(BibliotecaDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            var prestamos = await dbcontext.prestamo.ToListAsync();
            return Ok(prestamos);
        }

        [HttpPost]
        public async Task<ActionResult> PostCrearPrestamo([FromBody] Prestamo prestamo)
        {
            if (prestamo == null || prestamo.LibroId == Guid.Empty || prestamo.UsuarioId == Guid.Empty || prestamo.FechaPrestamo == default)
            {
                return BadRequest("Faltan datos requeridos: LibroId, UsuarioId y FechaPrestamo son obligatorios.");
            }
            prestamo.Id = Guid.NewGuid();
            prestamo.estado = "Activo";
            prestamo.FechaDevolucion = null;
            dbcontext.prestamo.Add(prestamo);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCrearPrestamo), new { id = prestamo.Id }, prestamo);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult> PostDevolverPrestamo(Guid id)
        {
            var prestamo = await dbcontext.prestamo.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.FechaDevolucion = DateTime.Now;
            prestamo.estado = "Devuelto";
            dbcontext.prestamo.Update(prestamo);
            await dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}
