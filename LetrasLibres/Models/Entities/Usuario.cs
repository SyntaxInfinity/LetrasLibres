namespace LetrasLibres.Models.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Rut {  get; set; }
        public required string Celular { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; }

        public Usuario()
        {
            
        }

        public Usuario(string nombre, string apellido, string rut, string celular)
        {
            Nombre = nombre;
            Apellido = apellido;
            Rut = rut;
            Celular = celular;
        }

    }
}
