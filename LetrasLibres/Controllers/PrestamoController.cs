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

        //Lista de prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            var prestamos = await dbcontext.Prestamo.ToListAsync();
            return Ok(prestamos);
        }

        //Registrar un nuevo préstamo (libro → usuario).
        [HttpPost]
        public async Task<ActionResult> PostCrearPrestamo([FromBody] Prestamo prestamo)
        {
            if (prestamo == null || prestamo.LibroId == Guid.Empty || prestamo.UsuarioId == Guid.Empty || prestamo.FechaPrestamo == default)
            {
                return BadRequest("Faltan datos requeridos: LibroId, UsuarioId y FechaPrestamo son obligatorios.");
            }
            prestamo.Id = Guid.NewGuid();
            prestamo.estado = "Prestado";
            prestamo.FechaDevolucion = null;
            dbcontext.Prestamo.Add(prestamo);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCrearPrestamo), new { id = prestamo.Id }, prestamo);
        }

        //Registrar la devolución de un libro.
        [HttpPost("{id}")]
        public async Task<ActionResult> PostDevolverPrestamo(Guid id)
        {
            var prestamo = await dbcontext.Prestamo.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.FechaDevolucion = DateTime.Now;
            prestamo.estado = "Devuelto";
            dbcontext.Prestamo.Update(prestamo);
            await dbcontext.SaveChangesAsync();
            return Ok(new { mensaje = $"El libro fue devuelto exitosamente"});
        }
    }
}
