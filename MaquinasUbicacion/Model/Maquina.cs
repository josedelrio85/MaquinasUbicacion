using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaquinasUbicacion.Model
{
    public class Maquina
    {
        public int id { get; set; }
        public string modelo { get; set; }
        public string tipo { get; set; }
        public string nSerie { get; set; }
        public int idUbicacion { get; set; }
        public int idCliente { get; set; }

        public Cliente cliente { get; set; }
        public Ubicacion ubicacion { get; set; }
    }
}
