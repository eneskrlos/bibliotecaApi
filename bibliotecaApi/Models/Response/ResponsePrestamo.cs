namespace bibliotecaApi.Models.Response
{
    public class ResponsePrestamo
    {
        public Guid Id { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string ISBN { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
