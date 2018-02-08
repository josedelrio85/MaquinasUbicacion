using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaquinasUbicacion.Model
{
    public class Ubicacion
    {
        //public Ubicacion()
        //{
        //    //cliente = new Cliente();
        //}
        public int id { get; set; }
        public string ubicacion { get; set; }

        public int idCliente { get; set; }
        public Cliente cliente { get; set; }
    }
}
