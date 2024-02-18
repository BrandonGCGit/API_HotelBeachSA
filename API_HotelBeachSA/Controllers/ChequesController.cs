using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBeachSA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChequesController : ControllerBase
    {
        private readonly DBContextGestionHotel _context;

        public ChequesController(DBContextGestionHotel context)
        {
            _context = context;
        }

        [HttpGet("Listado")]
        public async Task<List<Cheque>> Listado()
        {
            return await _context.Cheque.ToListAsync();
        }

        [HttpGet("Consultar")]
        public async Task<ActionResult<Cheque>> GetCheque(int Id)
        {
            var cheque = await _context.Cheque.FirstOrDefaultAsync(c => c.Id == Id);

            if (cheque == null)
            {
                return NotFound("Cheque no encontrado");
            }

            return cheque;
        }

        [HttpPost("Agregar")]
        public ActionResult<string> Agregar(Cheque cheque)
        {
            try
            {
                _context.Cheque.Add(cheque);
                _context.SaveChanges();
                return Ok("Cheque registrado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al registrar el cheque: " + ex.Message);
            }
        }

        [HttpPut("Modificar")]
        public ActionResult<string> Modificar(Cheque cheque)
        {
            try
            {
                _context.Cheque.Update(cheque);
                _context.SaveChanges();
                return Ok("Cheque modificado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al modificar el cheque: " + ex.Message);
            }
        }

        [HttpDelete("Eliminar")]
        public async Task<ActionResult<string>> Eliminar(int Id)
        {
            try
            {
                var cheque = await _context.Cheque.FirstOrDefaultAsync(c => c.Id == Id);
                if (cheque == null)
                {
                    return NotFound("Cheque no encontrado");
                }

                _context.Cheque.Remove(cheque);
                await _context.SaveChangesAsync();
                return Ok($"El cheque con ID {cheque.Id} ha sido eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el cheque: " + ex.Message);
            }
        }
    }
}
