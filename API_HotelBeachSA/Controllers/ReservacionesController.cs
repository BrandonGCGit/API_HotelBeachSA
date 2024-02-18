using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBeachSA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase
    {
        private readonly DBContextGestionHotel _context;

        public ReservacionesController(DBContextGestionHotel context)
        {
            _context = context;
        }

        [HttpGet("Listado")]
        public async Task<List<Reservacion>> Listado()
        {
            return await _context.Reservacion.ToListAsync();
        }

        [HttpGet("Consultar")]
        public async Task<ActionResult<Reservacion>> GetReservacion(int Id)
        {
            var reservacion = await _context.Reservacion.FirstOrDefaultAsync(u => u.Id == Id);

            return reservacion;
        }

        [HttpPost("Agregar")]
        public string Agregar(Reservacion reservacion)
        {
            string mensaje = "";
            try
            {
                _context.Reservacion.Add(reservacion);
                _context.SaveChanges();
                mensaje = "Reservacion registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.Message + " " + ex.InnerException.ToString();
            }

            return mensaje; //se retorna mensaje
        }

        [HttpPut("Modificar")]
        public string Modificar(Reservacion pReserve)
        {
            string mensaje = "";
            try
            {
                _context.Reservacion.Update(pReserve);
                _context.SaveChanges();
                mensaje = "Reserva modificado correctamente";

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
                var reservacion = await _context.Reservacion.FirstOrDefaultAsync(u => u.Id == Id);
                if (reservacion == null)
                {
                    mensaje = "No existe ningúna reserva bajo ese Id" + Id;
                }
                else
                {
                    _context.Reservacion.Remove(reservacion);
                    await _context.SaveChangesAsync();
                    mensaje = $"La reserva numero {reservacion.Id} ha sido eliminada correctamente";
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
