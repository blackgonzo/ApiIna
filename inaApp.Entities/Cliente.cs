using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{
    public class Cliente
    {

        public int Id { get; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }   

        public DateOnly FechaNac { get; set; }

        public Boolean Estado { get; set; }

    }
}
