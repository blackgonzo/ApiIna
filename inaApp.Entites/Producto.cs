using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inaApp.Entites
{
    [Table("tbProducto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="EL nombre es obligatorio.")]
        [StringLength(100,MinimumLength =3, ErrorMessage ="El nombre debe tener entre 3 y 100 carancteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="El precio es obligatorio.")]
        [Column(TypeName ="decimal(18,2)")]
        [Range(0.01,double.MaxValue,ErrorMessage ="EL precio debe ser mayour que cero.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage ="EL stock es obligatorio.")]
        [Range(1,int.MaxValue,ErrorMessage ="El stock no  puede ser un negatvo o 0.")]
        public int Stock { get; set; }

        [StringLength(500,ErrorMessage ="El maximo de caracteres para la descripción son 500 y el minimo 10.")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; } = true;

        //Relaciones
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; } = null!;
    }
}
