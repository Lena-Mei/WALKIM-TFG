using System.ComponentModel.DataAnnotations;

namespace AdminWALKIM.Models
{
    public class Mascota
    {
        public int idMascota { get; set; }
        [Required(ErrorMessage = "Debes de introducir la edad de la mascota.")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Edad no puede ser negativo.")]
        public int edad { get; set; }

        [Required(ErrorMessage = "Es necesario introducir un Nombre.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Nombre no puede contener números.")]
        public string nombre { get; set; } = null!;

        public string? imgMascota { get; set; }
        public int idTipoAnimal { get; set; }
        public int idUsuario { get; set; }
        [Required(ErrorMessage = "Es necesario introducir una descripción de la mascota.")]
        public string? descripcion { get; set; }
    }
}
