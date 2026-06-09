using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{

    [Table(name: "tCliente")]
    public class Cliente
    {




        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido1")]
        [StringLength(50)]
        public string Apellido1 { get; set; }

        [Required]
        [Column("apellido2")]
        [StringLength(50)]
        public string Apellido2 { get; set; }

        [Required]
        [Column("fecha_nac")]
        public DateOnly FechaNac { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

    }
}
