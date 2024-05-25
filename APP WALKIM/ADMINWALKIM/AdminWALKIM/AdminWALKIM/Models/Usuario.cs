using System.ComponentModel.DataAnnotations;

namespace AdminWALKIM.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        [Required(ErrorMessage = "Es necesario introducir el nombre.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Nombre no puede contener números.")]
        public string nombre { get; set; } = null!;
        [Required(ErrorMessage = "Es necesario introducir el primer apellido.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Apellido1 no puede contener números.")]
        public string apellido1 { get; set; } = null!;
        public string? apellido2 { get; set; }
        [Required(ErrorMessage = "Es necesario introducir un correo electrónico.")]
        [Display(Name = "Correo electrónico")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.(com|es)$", ErrorMessage = "Dirección de correo inválido. Debe tener '@' y terminar en '.com' o '.es'.")]
        public string correo { get; set; } = null!;
        public string? direccion { get; set; }
        public string? ciudad { get; set; }
        public string? provincia { get; set; }
        public string? codigoPostal { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo Teléfono solo puede contener números.")]
        [DataType(DataType.PhoneNumber)]
        public string? telefono { get; set; }
        public string contrasenya { get; set; } = null!;

        public string? imgPerfil { get; set; }
        [Required(ErrorMessage = "Es necesario escribir una descripción.")]
        public string? descripcion { get; set; }

        public List<Mascota>? mascotas { get; set; }
    }
}
