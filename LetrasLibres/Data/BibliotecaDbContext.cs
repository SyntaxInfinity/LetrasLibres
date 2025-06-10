using LetrasLibres.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LetrasLibres.Data
{
    public class BibliotecaDbContext : DbContext
    {
        //Configuración de DbContext 
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
