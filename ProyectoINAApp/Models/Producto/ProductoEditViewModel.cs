using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoINAApp.Models.Producto
{
    public class ProductoEditViewModel
    {
        [Required(ErrorMessage = "El Id es obligatorio.")]
        public int Id { get; set; }

        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Precio del Producto")]
        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, 10000.00, ErrorMessage = "El precio debe estar entre 0.01 y 10000.00.")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; } = 0;

        [Display(Name = "Stock del Producto")]
        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, 10000, ErrorMessage = "El stock debe estar entre 0 y 10000.")]
        public int Stock { get; set; } = 0;

        [Display(Name = "Descripcion del Producto")]
        [StringLength(500, ErrorMessage = "La descripcion no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "La categoria es obligatoria.")]
        public int CategoriaId { get; set; }

        public List<SelectListItem> Categorias { get; set; }
    }
}
