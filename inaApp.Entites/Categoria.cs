using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entites
{
    [Table(name: "tbCategoria")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(100,MinimumLength =3,ErrorMessage ="La categoria debe tener minimo 3 caracteres y maximo 100")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500,MinimumLength =10,ErrorMessage ="La descripcion debe tener minimo 10 caracteres y maximo 500")]
        public string Descripcion { get; set; }=string.Empty;

        public bool Estado { get; set; } = true;

        public DateTime Date { get; set; }=DateTime.Now;

        //Relaciones
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
