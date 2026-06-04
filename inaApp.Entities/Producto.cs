using System;
using System.Collections.Generic;
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
    public class Producto
    {
        //propiedades

        public int Id { get; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

    }
}
