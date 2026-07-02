using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static inaApp.Common.Enums.Enumeradores;

namespace inaApp.DTOs.cliente
{
    public class ClienteResponseDTO
    {
        public int IdCliente { get; set; }

        public TipoIdentificacionEnum TipoIdentificacion { get; set; }

        public string NumeroIdentificacion { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string PrimerApellido { get; set; } = string.Empty;

        public string? SegundoApellido { get; set; }

        public string CorreoElectronico { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;
    }
}
