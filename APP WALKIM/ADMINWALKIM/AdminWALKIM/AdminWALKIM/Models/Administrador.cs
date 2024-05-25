using System.ComponentModel.DataAnnotations;

namespace AdminWALKIM.Models
{
    public class Administrador
    {
        public int idAdministrador { get; set; }
        [Required(ErrorMessage = "Es necesario introducir el nombre.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Nombre no puede contener números.")]
        public string? nombre { get; set; }
        public string? correo { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo Teléfono solo puede contener números.")]
        [Required(ErrorMessage = "Es necesario introducir un número de teléfono.")]
        [DataType(DataType.PhoneNumber)]
        public string? telefono { get; set; }
        [Required(ErrorMessage = "Es necesario introducir el primer apellido.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Apellido1 no puede contener números.")]
        public string? apellido1 { get; set; }

        [Required(ErrorMessage = "Es necesario introducir el segundo apellido.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Apellido2 no puede contener números.")]
        public string? apellido2 { get; set; }
        public string? contrasenya { get; set; }
    }
}
