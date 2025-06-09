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
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.apellido) ||
                string.IsNullOrWhiteSpace(usuario.rut) || string.IsNullOrWhiteSpace(usuario.celular))
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
                .Include(u => u.Prestamo)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario.Prestamo);
        }

    }
}

