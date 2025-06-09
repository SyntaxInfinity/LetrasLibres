using LetrasLibres.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LetrasLibres.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {
        }

        public DbSet<Libro> libros { get; set; }
        public DbSet<Prestamo> prestamo { get; set; }
        public DbSet<Usuario> usuarios { get; set; }

    }
}
