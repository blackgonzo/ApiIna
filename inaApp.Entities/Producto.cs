using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{

    //niveles acceso
    //public: cualquier clase puede acceder a esta clase
    //private: solo las clases dentro del mismo archivo pueden acceder a esta clase
    //internal: solo las clases dentro del mismo proyecto pueden acceder a esta clase
    //protected: solo las clases dentro del mismo proyecto y las clases que heredan de


    [Table(name:"tbProduct")]
    public class Producto
    {
        //propiedades

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("stock")]
        public int Stock { get; set; }

        [StringLength(500)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

    }
}
