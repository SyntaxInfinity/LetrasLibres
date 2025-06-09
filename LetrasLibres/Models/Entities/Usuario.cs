namespace LetrasLibres.Models.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nombre { get; set; }
        public required string apellido { get; set; }
        public required string rut {  get; set; }
        public required string celular { get; set; }
    }
}
