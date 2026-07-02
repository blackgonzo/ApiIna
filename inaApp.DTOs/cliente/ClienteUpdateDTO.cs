using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static inaApp.Common.Enums.Enumeradores;

namespace inaApp.DTOs.cliente
{
    public class ClienteUpdateDTO
    {
        [Required(ErrorMessage = "El tipo de identificación es requerido")]
        [EnumDataType(typeof(TipoIdentificacionEnum), ErrorMessage = "Se debe enviar un tipo de identificación permitida")]
        public TipoIdentificacionEnum? TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "El número de identificación es requerido")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "El número de identificación debe ser mayor a 0")]
        public string? NumeroIdentificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener un minimo de 3 caracteres y un maximo de 100 caracteres")]
        public string? Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido del cliente es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El primer apellido debe tener un minimo de 3 caracteres y un maximo de 100 caracteres")]
        public string? PrimerApellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El segundo apellido del cliente es requeriod")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El segundo apellido debe tener un minimo de 3 caracteres y un maximo de 100 caracteres")]
        public string? SegundoApellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo del cliente es requerido")]
        [EmailAddress(ErrorMessage = "El correo debe respetar formato de correo")]
        public string? CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "EL telefono del cliente es requerido")]
        [Phone(ErrorMessage = "El telefono de respetar formato de telefono")]
        public string? Telefono { get; set; } = string.Empty;
    }
}
