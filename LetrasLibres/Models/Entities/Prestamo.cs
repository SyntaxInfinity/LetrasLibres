using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LetrasLibres.Models.Entities
{
    public class Prestamo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LibroId { get; set; }
        public Guid UsuarioId { get; set; }
        public string estado { get; set; } = "Activo";
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime? FechaDevolucion { get; set; }
    }
}
