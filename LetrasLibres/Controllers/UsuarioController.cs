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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await dbcontext.usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostCrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.apellido) ||
                string.IsNullOrWhiteSpace(usuario.rut) || string.IsNullOrWhiteSpace(usuario.celular))
            {
                return BadRequest("Faltan datos requeridos: nombre, apellido, rut y celular son obligatorios.");
            }
            usuario.Id = Guid.NewGuid();
            dbcontext.usuarios.Add(usuario);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioId(Guid id)
        {
            var usuario = await dbcontext.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
    }
}

