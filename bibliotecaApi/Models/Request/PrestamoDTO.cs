namespace bibliotecaApi.Models.Request
{
    public class PrestamoDTO
    {
        public Guid Id { get; set; }
        public Guid IdLibro { get; set; }
        public Guid IdLector { get; set; }
        public DateTime FechaPrestamo { get; set; }
    }
}
