using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bibliotecaApi.Models
{
    public partial class Lector
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido"), 
            MaxLength(255, ErrorMessage = "El campo nombre no debe tener mas de 255 caracteres.")]
        public string Nombre { get; set; } = null!;
        [JsonIgnore]
        public virtual List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
 