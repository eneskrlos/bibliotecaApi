using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bibliotecaApi.Models
{
    public partial class Prestamo
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo fecha es requerido")]
        public DateTime FechaPrestamo { get; set; }
        public Guid IdLibro { get; set; }
        [JsonInclude]
        public Libro LibroNavigation { get; set; } = new Libro();
        public Guid LectorId { get; set; }
        [JsonInclude]
        public virtual Lector LectorNavigation { get; set; } = new Lector();

    }
}
