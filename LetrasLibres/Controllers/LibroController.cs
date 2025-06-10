using LetrasLibres.Data;
using LetrasLibres.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LetrasLibres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly BibliotecaDbContext dbcontext;

        public LibroController(BibliotecaDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        //obtiene la lista de los libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            var libros = await dbcontext.Libros.ToListAsync();
            return Ok(libros);
        }

        //obtiene libros por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibrosId(Guid id)
        {
            var libro = await dbcontext.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        //Crea un nuevo libro.
        [HttpPost]
        public async Task<ActionResult<Libro>> PostCrearLibro([FromBody] Libro libro)
        {
            if (libro == null)
            {
                return BadRequest("Datos no válidos.");
            }

            // Validar título y autor
            if (string.IsNullOrWhiteSpace(libro.Titulo) || string.IsNullOrWhiteSpace(libro.Autor))
            {
                return BadRequest("Faltan datos requeridos: título y autor son obligatorios.");
            }

            // Validar ISBN (debe tener 10 o 13 caracteres y ser numérico)
            if (string.IsNullOrWhiteSpace(libro.Isbn) || !(libro.Isbn.Length == 10 || libro.Isbn.Length == 13) || !libro.Isbn.All(char.IsDigit))
            {
                return BadRequest("El ISBN debe contener 10 o 13 dígitos numéricos.");
            }

            // Validar fecha de publicación (no puede ser en el futuro)
            if (libro.Publicacion > DateTime.Now)
            {
                return BadRequest("La fecha de publicación no puede ser una fecha futura.");
            }

            libro.Id = Guid.NewGuid();
            dbcontext.Libros.Add(libro);
            await dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibrosId), new { id = libro.Id }, libro);
        }

        //Edita la información de un libro.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActualizarLibro(Guid id, [FromBody] Libro libro)
        {
            if (libro == null || id != libro.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo.");
            }

            var libroExistente = await dbcontext.Libros.FindAsync(id);
            if (libroExistente == null)
            {
                return NotFound($"No se encontró un libro con ID: {id}");
            }

            if (string.IsNullOrWhiteSpace(libro.Titulo) || string.IsNullOrWhiteSpace(libro.Autor))
            {
                return BadRequest("Los campos 'Título' y 'Autor' son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(libro.Isbn) || !(libro.Isbn.Length == 10 || libro.Isbn.Length == 13) || !libro.Isbn.All(char.IsDigit))
            {
                return BadRequest("El ISBN debe contener exactamente 10 o 13 dígitos numéricos.");
            }

            if (libro.Publicacion > DateTime.Now)
            {
                return BadRequest("La fecha de publicación no puede ser una fecha futura.");
            }

            //cambia valor por valor para modificar
            libroExistente.Titulo = libro.Titulo;
            libroExistente.Publicacion = libro.Publicacion;
            libroExistente.Autor = libro.Autor;
            libroExistente.Isbn = libro.Isbn;
            libroExistente.Categoria = libro.Categoria;

            await dbcontext.SaveChangesAsync();
            return NoContent();
        }


        //Elimina un libro (solo si no está prestado).
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLibro(Guid id)
        {
            var libro = await dbcontext.Libros.FindAsync(id);
            if(libro == null)
            {
                return NotFound($"No se encontró el libro con el id {id}");
            }
            var prestamoActivo = await dbcontext.Prestamos.AnyAsync(p => p.LibroId == id && p.FechaDevolucion == null);
            if (prestamoActivo)
            {
                return BadRequest("No se puede eliminar el libro porque está prestado actualmente");
            }
                dbcontext.Libros.Remove(libro);
            await dbcontext.SaveChangesAsync();

            return Ok(new { mensaje = $"El libro con ID {id} fue eliminado correctamente." });
        }
    }
}
