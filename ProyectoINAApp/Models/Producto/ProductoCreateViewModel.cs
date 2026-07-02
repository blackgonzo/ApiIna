using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoINAApp.Models.Producto
{
    public class ProductoCreateViewModel
    {
        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Precio del Producto")]
        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; } = 0;

        [Display(Name = "Stock del Producto")]
        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El stock no puede ser negativo o 0.")]
        public int Stock { get; set; } = 0;

        [Display(Name = "Descripcion del Producto")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripcion debe tener entre 10 y 500 caracteres.")]
        public string Descripcion { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "La categoria es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoria valida.")]
        public int CategoriaId { get; set; }

        public List<SelectListItem> Categorias { get; set; }
    }
}
