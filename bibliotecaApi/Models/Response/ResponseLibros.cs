namespace bibliotecaApi.Models.Response
{
    public class ResponseLibros
    {
        
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public bool Prestado { get; set; }
    }
}
