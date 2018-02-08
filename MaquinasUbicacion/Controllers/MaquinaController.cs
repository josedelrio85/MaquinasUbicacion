using System.Collections.Generic;
using System.Linq;
using MaquinasUbicacion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaquinasUbicacion.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MaquinaController : Controller
    {
        private readonly maqUbiContext _context;

        public MaquinaController(maqUbiContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet("", Name = "GetAllMaquinas")]
        public IEnumerable<Maquina> GetAll()
        {
            IEnumerable<Maquina> lista = _context.Maquina
                .Include(a => a.ubicacion)
                .Include(b => b.cliente)
                .ToList();
            return lista;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetMaquinaById")]
        public IActionResult GetById(int id)
        {
            var item = _context.Maquina
                .Include(a => a.ubicacion)
                .FirstOrDefault(x => x.id == id);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //GET api/Maquina/GetMaxId
        [HttpGet("GetMaxId", Name = "GetMaquinaMaxId")]
        public IActionResult GetMaxId()
        {
            var item = _context.Maquina
                .Include(a => a.cliente)
                .Include(b => b.ubicacion)
                .OrderByDescending(u => u.id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("SearchMaquina/{param}", Name = "SearchMaquina")]
        public IEnumerable<Maquina> SearchMaquina(string param)
        {
            IEnumerable<Maquina> lista = _context.Maquina
                .Include(a => a.ubicacion)
                .Include(b => b.cliente)
                .Where(x => x.modelo.Contains(param))
                .ToList();

            return lista;
        }

        [HttpGet("ordena/{param}/{desc}", Name = "Ordena")]
        public IEnumerable<Maquina> Ordena(string param, bool? desc)
        {
            IEnumerable<Maquina> lista = _context.Maquina
                .Include(a => a.ubicacion)
                .Include(b => b.cliente);

            switch (param)
            {
                case "modelo":
                    lista = (desc == false) ? lista.OrderBy(x => x.modelo) : lista.OrderByDescending(x => x.modelo);
                    break;
                case "tipo":
                    lista = (desc == false) ? lista.OrderBy(x => x.tipo) : lista.OrderByDescending(x => x.tipo);
                    break;
                case "nSerie":
                    lista = (desc == false) ? lista.OrderBy(x => x.nSerie) : lista.OrderByDescending(x => x.nSerie);
                    break;
                default:
                    break;
            }

            return lista.ToList();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Maquina maquina)
        {
            if (maquina == null)
            {
                return NotFound();
            }

            if (maquina.cliente == null)
            {
                maquina.cliente = _context.Cliente.FirstOrDefault(p => p.id == maquina.idCliente);
            }

            if (maquina.ubicacion == null)
            {
                maquina.ubicacion = _context.Ubicacion.FirstOrDefault(p => p.id == maquina.idUbicacion);
            }
            _context.Maquina.Add(maquina);
            _context.SaveChanges();

            return new ObjectResult(maquina);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Maquina item)
        {
            if (id != item.id || item == null)
            {
                return BadRequest();
            }

            var element = _context.Maquina
                .Include(a => a.ubicacion)
                .Include(b => b.cliente)
                .FirstOrDefault(t => t.id == item.id);

            if (element == null)
            {
                return NotFound();
            }

            element.modelo = item.modelo;
            element.nSerie = item.nSerie;
            element.tipo = item.tipo;
            element.idCliente = item.idCliente;
            element.idUbicacion = item.idUbicacion;

            _context.Maquina.Update(element);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var element = _context.Maquina.FirstOrDefault(t => t.id == id);
            if (element == null)
            {
                return NotFound();
            }

            _context.Maquina.Remove(element);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}