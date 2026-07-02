using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.DTOs.Categoria
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "El nombre de la categoria es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "La categoria debe tener minimo 3 caracteres y maximo 100")]
        public string Nombre { get; set; }=String.Empty;

        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripcion debe tener minimo 10 caracteres y maximo 500")]
        public string Descripcion { get;set; }=String.Empty;
    }
}
