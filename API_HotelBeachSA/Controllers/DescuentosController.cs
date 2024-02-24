using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBeachSA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescuentosController : Controller
    {
        private readonly DBContextGestionHotel _context;

        public DescuentosController(DBContextGestionHotel pContext)
        {
            _context = pContext;
        }

        /// <summary>
        /// Método para mostrar el listado de Descuentos
        /// </summary>
        /// <returns></returns>
        [HttpGet("Listado")]
        public async Task<List<Discount>> Listado()
        {
            var list = await _context.Descuento.ToListAsync();
            return list;
        }//Listado


        /// <summary>
        /// Método para Consultar
        /// </summary>
        /// <param name="Cedula"></param>
        /// <returns></returns>
        [HttpGet("Consultar")]
        public async Task<Discount> Consultar(int id)
        {
            var temp = await _context.Descuento.FirstOrDefaultAsync(u => u.Id == id);
            return temp;
        }//Consultar


        /// <summary>
        /// Método para agregar Descuentos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("Agregar")]
        public string Agregar(Discount descuento)
        {
            string mensaje = "";
            try
            {
                _context.Descuento.Add(descuento);
                _context.SaveChanges();
                mensaje = "Descuento registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.Message + " " + ex.InnerException.ToString();
            }
            return mensaje;
        }//Agregar

        /// <summary>
        /// Método para modificar un Descuento
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("Modificar")]
        public string Modificar(Discount descuento)
        {
            string mensaje = "";
            try
            {
                _context.Descuento.Update(descuento);
                _context.SaveChanges();
                mensaje = "Descuento modificado correctamente";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }

            return mensaje;
        }//Modificar


        /// <summary>
        /// Método para eliminar un Descuento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Eliminar")]
        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {
                var temp = await _context.Descuento.FirstOrDefaultAsync(u => u.Id == id);
                if (temp == null)
                {
                    mensaje = "No existe ningún descuento con el id " + id;
                }
                else
                {
                    _context.Descuento.Remove(temp);
                    await _context.SaveChangesAsync();
                    mensaje = $"Descuento # {temp.Id} eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }
            return mensaje;
        }//Eliminar

    }//class
}//namespace
