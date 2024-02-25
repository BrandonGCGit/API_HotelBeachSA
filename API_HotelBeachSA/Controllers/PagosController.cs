using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBeachSA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : Controller
    {
        private readonly DBContextGestionHotel _context;

        public PagosController(DBContextGestionHotel context)
        {
            _context = context;
        }

        [HttpGet("Listado")]
        public async Task<List<Pago>> Listado()
        {
            return await _context.Pago.ToListAsync();
        }

        [HttpGet("Consultar")]
        public async Task<ActionResult<Pago>> GetPago(int Id)
        {
            var Pago = await _context.Pago.FirstOrDefaultAsync(u => u.Id == Id);

            return Pago;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<Reservacion>> Agregar(Pago pago)
        {
            string mensaje = "";
            try
            {
                _context.Pago.Add(pago);
                _context.SaveChanges();
                mensaje = "Pago registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.Message + " " + ex.InnerException.ToString();
            }

            return CreatedAtAction("GetPago", new { id = pago.Id }, pago);
        }

        [HttpPut("Modificar")]
        public string Modificar(Pago pPago)
        {
            string mensaje = "";
            try
            {
                _context.Pago.Update(pPago);
                _context.SaveChanges();
                mensaje = "Pago modificado correctamente";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }

            return mensaje;
        }

        [HttpDelete("Eliminar")]
        public async Task<string> Eliminar(int Id)
        {
            string mensaje = "";
            try
            {
                var pago = await _context.Pago.FirstOrDefaultAsync(u => u.Id == Id);
                if (pago == null)
                {
                    mensaje = "No existe ningún pago bajo ese Id" + Id;
                }
                else
                {
                    _context.Pago.Remove(pago);
                    await _context.SaveChangesAsync();
                    mensaje = $"El pago numero {pago.Id} ha sido eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }
            return mensaje;
        }
    }
}
