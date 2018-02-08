using System.Collections.Generic;
using System.Linq;
using MaquinasUbicacion.Model;
using Microsoft.AspNetCore.Mvc;

namespace MaquinasUbicacion.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly maqUbiContext _context;

        public ClientesController(maqUbiContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet("", Name = "GetAllClientes")]
        public IEnumerable<Cliente> GetAll()
        {
            IEnumerable<Cliente> lista = _context.Cliente.ToList();
            return lista;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetClienteById")]
        public IActionResult GetById(int id)
        {
            var item = _context.Cliente.FirstOrDefault(x => x.id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("GetMaxId", Name = "GetClienteMaxId")]
        public IActionResult GetMaxId()
        {
            var item = _context.Cliente
                .OrderByDescending(u => u.id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("SearchClientes/{param}", Name = "SearchClientes")]
        public IEnumerable<Cliente> SearchClientes(string param)
        {
            IEnumerable<Cliente> lista = _context.Cliente.Where(x => x.nombre.Contains(param)).ToList();

            return lista;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Cliente cliente)
        {
            if(cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Add(cliente);
            _context.SaveChanges();

            return new ObjectResult(cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Cliente item)
        {
            if (id != item.id || item == null)
            {
                return BadRequest();
            }

            var element = _context.Cliente.FirstOrDefault(t => t.id == item.id);
            if (element == null)
            {
                return NotFound();
            }

            element.nombre = item.nombre;

            _context.Cliente.Update(element);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var element = _context.Cliente.FirstOrDefault(t => t.id == id);
            if (element == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(element);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
