using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;

namespace API_HotelBeachSA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaquetesController : ControllerBase
    {
        private readonly DBContextGestionHotel _context;

        public PaquetesController(DBContextGestionHotel context)
        {
            _context = context;
        }

        // GET: api/Paquetes
        [HttpGet("Listado")]
        public async Task<ActionResult<IEnumerable<Paquete>>> GetPaquete()
        {
            return await _context.Paquete.ToListAsync();
        }

        // GET: api/Paquetes/5
        [HttpGet("Constultar")]
        public async Task<ActionResult<Paquete>> GetPaquete(int id)
        {
            var paquete = await _context.Paquete.FindAsync(id);

            if (paquete == null)
            {
                return NotFound();
            }

            return paquete;
        }

        // PUT: api/Paquetes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Editar")]
        public async Task<IActionResult> PutPaquete(int id, Paquete paquete)
        {
            if (id != paquete.Id)
            {
                return BadRequest();
            }

            _context.Entry(paquete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaqueteExists(id))
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

        // POST: api/Paquetes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Crear")]
        public async Task<ActionResult<Paquete>> PostPaquete(Paquete paquete)
        {
            _context.Paquete.Add(paquete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaquete", new { id = paquete.Id }, paquete);
        }

        // DELETE: api/Paquetes/5
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> DeletePaquete(int id)
        {
            var paquete = await _context.Paquete.FindAsync(id);
            if (paquete == null)
            {
                return NotFound();
            }

            _context.Paquete.Remove(paquete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaqueteExists(int id)
        {
            return _context.Paquete.Any(e => e.Id == id);
        }
    }
}
