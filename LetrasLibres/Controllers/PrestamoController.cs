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
            var prestamos = await dbcontext.Prestamos.ToListAsync();
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
            prestamo.Estado = "Prestado";
            prestamo.FechaDevolucion = null;
            dbcontext.Prestamos.Add(prestamo);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCrearPrestamo), new { id = prestamo.Id }, prestamo);
        }

        //Registrar la devolución de un libro.
        [HttpPost("{id}")]
        public async Task<ActionResult> PostDevolverPrestamo(Guid id)
        {
            var prestamo = await dbcontext.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.FechaDevolucion = DateTime.Now;
            prestamo.Estado = "Devuelto";
            dbcontext.Prestamos.Update(prestamo);
            await dbcontext.SaveChangesAsync();
            return Ok(new { mensaje = $"El libro fue devuelto exitosamente"});
        }

        //Edita la información de un prestamo.
        // Edita la información de un préstamo.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActualizarPrestamos(Guid id, [FromBody] Prestamo prestamo)
        {
            if (prestamo == null || id != prestamo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo.");
            }

            var prestamoExistente = await dbcontext.Prestamos.FindAsync(id);
            if (prestamoExistente == null)
            {
                return NotFound($"No se encuentra el préstamo con id {id}");
            }

            if (prestamo.LibroId == Guid.Empty)
            {
                return BadRequest("El campo 'LibroId' es obligatorio.");
            }
            if (prestamo.UsuarioId == Guid.Empty)
            {
                return BadRequest("El campo 'UsuarioId' es obligatorio.");
            }

            // Actualiza los campos del préstamo existente con los valores nuevos
            prestamoExistente.LibroId = prestamo.LibroId;
            prestamoExistente.UsuarioId = prestamo.UsuarioId; 
            prestamoExistente.FechaPrestamo = prestamo.FechaPrestamo;
            prestamoExistente.FechaDevolucion = prestamo.FechaDevolucion;

            await dbcontext.SaveChangesAsync();
            return NoContent();
        }


        //Eliminar un prestamo
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrestamo(Guid id)
        {
            var prestamo = await dbcontext.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound($"El usuario no fue encontrado, ingrese una nueva id");
            }
            dbcontext.Prestamos.Remove(prestamo);
            await dbcontext.SaveChangesAsync();

            return Ok(new { mensaje = $"El prestamo fue borrado exitosamente" });
        }
    }

}
