using System.Text.Json.Serialization;

namespace LetrasLibres.Models.Entities
{
    public class Libro
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public  required string Titulo { get; set; } = String.Empty;
        public required DateTime Publicacion { get; set; }
        public required string Autor { get; set; } = String.Empty;
        public required string Isbn {  get; set; } = String.Empty;
        public required string Categoria { get; set; } = String.Empty;
    }


}
