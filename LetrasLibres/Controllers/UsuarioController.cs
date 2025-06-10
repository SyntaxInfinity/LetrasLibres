using LetrasLibres.Data;
using LetrasLibres.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LetrasLibres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BibliotecaDbContext dbcontext;

        public UsuarioController(BibliotecaDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        //Lista de usuarios registrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await dbcontext.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        //Registrar un nuevo usuario.
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostCrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Apellido) ||
                string.IsNullOrWhiteSpace(usuario.Rut) || string.IsNullOrWhiteSpace(usuario.Celular))
            {
                return BadRequest("Faltan datos requeridos: nombre, apellido, rut y celular son obligatorios.");
            }
            usuario.Id = Guid.NewGuid();
            dbcontext.Usuarios.Add(usuario);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
        }

        //Ver el historial de préstamos del usuario.
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetHistorialPrestamos(Guid id)
        {
            var usuario = await dbcontext.Usuarios
                .Include(u => u.Prestamos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario.Prestamos);
        }

        //Edita la información de un usuario.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActualizarUsuarios(Guid id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo.");
            }

            var usuarioExistente = await dbcontext.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound($"No se ecuentra el usuario con id {id}");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                return BadRequest("Los campos 'Nombre' y 'Apellido' son obligatorios.");
            }


            //cambia valor por valor para modificar
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Rut = usuario.Rut;
            usuarioExistente.Celular = usuario.Celular;

            await dbcontext.SaveChangesAsync();
            return NoContent();
        }

        //Eliminar un usuario
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await dbcontext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound($"El usuario no fue encontrado, ingrese una nueva id");
            }
            dbcontext.Usuarios.Remove(usuario);
            await dbcontext.SaveChangesAsync();

            return Ok(new { mensaje = $"El usuario fue borrado exitosamente" });
        }
    }
}

