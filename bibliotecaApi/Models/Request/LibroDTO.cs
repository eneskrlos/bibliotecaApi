namespace bibliotecaApi.Models.Request
{
    public class LibroDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty!;
        public string ISBN { get; set; } = string.Empty!;
        public bool Prestado { get; set; }
    }
}
