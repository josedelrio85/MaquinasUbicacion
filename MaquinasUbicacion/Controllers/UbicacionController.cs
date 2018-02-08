using System.Collections.Generic;
using System.Linq;
//using System.Web.Http;
using MaquinasUbicacion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaquinasUbicacion.Controllers
{
    [Produces("application/json")]
    //[Route("api/ubicaciones")]
    [Route("api/[controller]")]
    public class UbicacionController : Controller
    {
        private readonly maqUbiContext _context;

        public UbicacionController(maqUbiContext context)
        {
            _context = context;
        }

        //GET api/Ubicacion
        [HttpGet("", Name = "GetAllUbicaciones")]
        public IEnumerable<Ubicacion> GetAll()
        {
            IEnumerable<Ubicacion> lista = _context.Ubicacion
                .Include(a => a.cliente)
                .ToList();
            return lista;
        }

        //GET api/Ubicacion/id
        [HttpGet("{id}", Name = "GetUbicacionById")]
        public IActionResult GetById(int id)
        {
            var item = _context.Ubicacion
                .Include(a => a.cliente)
                .FirstOrDefault(x => x.id == id);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //GET api/Ubicacion/GetMaxId
        [HttpGet("GetMaxId", Name = "GetUbicacionMaxId")]
        public IActionResult GetMaxId()
        {
            var item = _context.Ubicacion
                .Include(a => a.cliente)
                .OrderByDescending(u => u.id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("SearchUbicacion/{param}", Name = "SearchUbicacion")]
        public IEnumerable<Ubicacion> SearchUbicacion(string param)
        {
            IEnumerable<Ubicacion> lista = _context.Ubicacion
                    .Include(a => a.cliente)
                    .Where(x => x.ubicacion.Contains(param))
                    .ToList();

            return lista;
        }

        ////GET api/ordena/GetMaxId
        //[HttpGet("ordena", Name = "GetUbicacionMaxId")]
        //public IActionResult GetMaxId()
        //{
        //    var item = _context.Ubicacion
        //        .Include(a => a.cliente)
        //        .OrderByDescending(u => u.id).FirstOrDefault();

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return new ObjectResult(item);
        //}

        //GET api/Ubicacion/ubicacionescliente
        [HttpGet("ubicacionescliente/{idCliente}", Name = "ubicacionescliente")]
        public IEnumerable<Ubicacion> GetUbicacionesCliente(int idCliente)
        {
            var item = _context.Ubicacion
                //.Include(a => a.cliente)
                .Where(a => a.idCliente == idCliente)
                .OrderByDescending(u => u.id)
                .ToList();
            
            return item;
        }

        //POST api/Ubicacion
        [HttpPost]
        public IActionResult Create([FromBody]Ubicacion Ubicacion)
        {
            if (Ubicacion == null)
            {
                return NotFound();
            }

            if(Ubicacion.cliente == null)
            {
                Ubicacion.cliente = _context.Cliente.FirstOrDefault(p => p.id == Ubicacion.idCliente);
            }

            _context.Ubicacion.Add(Ubicacion);
            _context.SaveChanges();

            //return CreatedAtRoute("GetById", new { id = Ubicacion.id }, Ubicacion);
            return new NoContentResult();
        }

        //PUT api/Ubicacion/id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Ubicacion item)
        {
            if (id != item.id || item == null)
            {
                return BadRequest();
            }

            var element = _context.Ubicacion
                .FirstOrDefault(t => t.id == item.id);

            if (element == null)
            {
                return NotFound();
            }

            element.ubicacion = item.ubicacion;
            element.idCliente = item.idCliente;

            _context.Ubicacion.Update(element);
            _context.SaveChanges();
            return new NoContentResult();
        }

        //DELETE api/Ubicacion/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var element = _context.Ubicacion.FirstOrDefault(t => t.id == id);
            if (element == null)
            {
                return NotFound();
            }

            _context.Ubicacion.Remove(element);
            _context.SaveChanges();
            return new NoContentResult();
        }
    
    }
}