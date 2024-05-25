using System.ComponentModel.DataAnnotations;

namespace AdminWALKIM.Models
{
    public class Servicio
    {
        public int idServicio { get; set; }
        [RegularExpression(@"^[-0123456789]+[0-9.,]*$", ErrorMessage = "El valor introducido debe ser de tipo monetario.")]
        [Required(ErrorMessage = "Debes de introducir un precio al producto.")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio no puede ser negativo.")]
        public decimal precio { get; set; }

        [Required(ErrorMessage = "Es necesario introducir un Nombre.")]
        public string nombre { get; set; } = null!;
        [Required(ErrorMessage = "Es necesario introducir una descripción del servicio.")]
        public string descripcion { get; set; } = null!;
        public decimal puntaje { get; set; }
        public int idTipoServicio { get; set; }
        public int idServidor { get; set; }

        [Required(ErrorMessage = "Es necesario escoger como mínimo 1 tipo de animal.")]
        public List<TipoAnimalServicio> aceptaTipo { get; set; }
        public List<Contrata>? contratos { get; set; }
    }
}
