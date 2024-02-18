using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBeachSA.Controllers
{
    public class RolesController : Controller
    {
        private readonly DBContextGestionHotel _context;

        public RolesController(DBContextGestionHotel pContext)
        {
            _context = pContext;
        }

        /// <summary>
        /// Método para mostrar el listado de Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("Listado")]
        public async Task<List<Rol>> Listado()
        {
            var list = await _context.Rol.ToListAsync();
            return list;
        }//Listado


        /// <summary>
        /// Método para consultar Rol
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consultar")]
        public async Task<Rol> Consultar(int id)
        {
            var temp = await _context.Rol.FirstOrDefaultAsync(u => u.Id == id);
            return temp;
        }//Consultar

        /// <summary>
        /// Método para agregar roles
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        [HttpPost("Agregar")]
        public string Agregar(Rol rol)
        {
            string mensaje = "";
            try
            {
                _context.Rol.Add(rol);
                _context.SaveChanges();
                mensaje = "Rol registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.Message + " " + ex.InnerException.ToString();
            }
            return mensaje;
        }//Agregar


        /// <summary>
        /// Método para modificar roles
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        [HttpPut("Modificar")]
        public string Modificar(Rol rol)
        {
            string mensaje = "";
            try
            {
                _context.Rol.Update(rol);
                _context.SaveChanges();
                mensaje = "Rol modificado correctamente";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }

            return mensaje;
        }//Modificar

        [HttpDelete("Eliminar")]
        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {
                var temp = await _context.Rol.FirstOrDefaultAsync(u => u.Id == id);
                if (temp == null)
                {
                    mensaje = "No existe ningún rol con el id " + id;
                }
                else
                {
                    _context.Rol.Remove(temp);
                    await _context.SaveChangesAsync();
                    mensaje = $"Rol # {temp.Id} eliminado correctamente";
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
