using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad18.Server.Contexto;
using Actividad18.Shared.Models;

namespace Actividad_18.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ContextoTiendaLinea _context;

        public ClientesController(ContextoTiendaLinea context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var clientes = await _context.Clientes.Include(u => u.Pedidos).
                 ThenInclude(p => p.Productos).
                 ToListAsync();

            return clientes;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            // Incluir los préstamos vinculados junto con los libros
            var clientes = await _context.Clientes
                .Include(u => u.Pedidos)
                    .ThenInclude(p => p.Productos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int? id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'ContextoTiendaLinea.Clientes' is null.");
            }
            _context.Clientes.Add(clientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientes", new { id = clientes.Id }, clientes);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int? id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.Include(u => u.Pedidos).FirstOrDefaultAsync(u => u.Id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            if (clientes.Pedidos != null && clientes.Pedidos.Any())
            {
                var pedidosId = clientes.Pedidos.Select(p => p.Id).ToList();
                var productos = await _context.Productos.Where(l => pedidosId.Contains((int)l.PedidosId)).ToListAsync();

                foreach (var producto in productos)
                {
                    producto.PedidosId = null;
                }

                _context.Pedidos.RemoveRange(clientes.Pedidos);
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesExists(int? id)
        {
            return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
