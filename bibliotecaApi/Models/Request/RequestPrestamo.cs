namespace bibliotecaApi.Models.Request
{
    public class RequestPrestamo
    {
        public Guid IdLibro { get; set; }
        public Guid IdLector { get; set; }
        public DateTime FechaPrestamo { get; set; }
    }
}
