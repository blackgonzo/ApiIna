using System.ComponentModel.DataAnnotations;

namespace ProyectoINAApp.Models.Categoria
{
    public class CategoriaCreateViewModel
    {
        [Display(Name = "Nombre de la categoria")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Descripcion")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripcion debe tener entre 10 y 500 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
