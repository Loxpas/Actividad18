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
    public class PedidosController : ControllerBase
    {
        private readonly ContextoTiendaLinea _context;

        public PedidosController(ContextoTiendaLinea context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedidos>>> GetPedidos()
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            return await _context.Pedidos.Include(p => p.Clientes).Include(p => p.Productos).ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedidos>> GetPedidos(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedidos = await _context.Pedidos.Include(p => p.Clientes).Include(p => p.Productos
            ).FirstOrDefaultAsync(p => p.Id == id);

            if (pedidos == null)
            {
                return NotFound();
            }

            return pedidos;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidos(int id, Pedidos pedidos)
        {
            if (id != pedidos.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidosExists(id))
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

        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedidos>> PostPrestamos(Pedidos pedidos)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'ContextoTiendaLinea.Prestamos' is null.");
            }

            // Obtener el usuario correspondiente al ID proporcionado
            var cliente = await _context.Clientes.FindAsync(pedidos.ClientesId);
            if (cliente == null)
            {
                return BadRequest("El cliente en especificado no existe.");
            }

            pedidos.Clientes = cliente;

            _context.Pedidos.Add(pedidos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidos", new { id = pedidos.Id }, pedidos);
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidos(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedidos = await _context.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedidos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidosExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
