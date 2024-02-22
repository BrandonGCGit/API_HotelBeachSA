using API_HotelBeachSA.Context;
using API_HotelBeachSA.Models;
using API_HotelBeachSA.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBeachSA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly DBContextGestionHotel _context;

        private readonly IAutorizacionServices _autorizacionServices;

        public UsuariosController(DBContextGestionHotel pContext, IAutorizacionServices autorizacionServices)
        {
            _context = pContext;

            _autorizacionServices = autorizacionServices;
        }

        /// <summary>
        /// Método para mostrar el listado de Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("Listado")]
        public async Task<List<Usuario>> Listado()
        {
            var list = await _context.Usuario.ToListAsync();
            return list;
        }//Listado

        /// <summary>
        /// Método para Consultar por cédula
        /// </summary>
        /// <param name="Cedula"></param>
        /// <returns></returns>
        [HttpGet("Consultar")]
        public async Task<Usuario> Consultar(string Cedula)
        {
            var temp = await _context.Usuario.FirstOrDefaultAsync(u => u.Cedula == Cedula);
            return temp;
        }//Consultar

        /// <summary>
        /// Método para Consultar por cédula
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpGet("ConsultarPorEmail")]
        public async Task<Usuario> ConsultarPorEmail(string email)
        {
            var temp = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
            return temp;
        }//Consultar

        /// <summary>
        /// Método para agregar un Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Agregar")]
        public string Agregar(Usuario usuario)
        {
            string mensaje = "";
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                mensaje = "Usuario registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.Message + " " + ex.InnerException.ToString();
            }
            return mensaje; //se retorna el mensaje
        }//Agregar


        /// <summary>
        /// Método para modificar un Usuario
        /// </summary>
        /// <param name="pUser"></param>
        /// <returns></returns>
        [HttpPut("Modificar")]
        public string Modificar(Usuario pUser)
        {
            string mensaje = "";
            try
            {
                _context.Usuario.Update(pUser);
                _context.SaveChanges();
                mensaje = "Usuario modificado correctamente";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }

            return mensaje;
        }//Modificar

        [HttpDelete("Eliminar")]
        public async Task<string> Eliminar(string Cedula)
        {
            string mensaje = "";
            try
            {
                var temp = await _context.Usuario.FirstOrDefaultAsync(u => u.Cedula == Cedula);
                if (temp == null)
                {
                    mensaje = "No existe ningún usuario con la cédula " + Cedula;
                }
                else
                {
                    _context.Usuario.Remove(temp);
                    await _context.SaveChangesAsync();
                    mensaje = $"Usuario {temp.Primer_Apellido} eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " " + ex.InnerException.ToString();
            }
            return mensaje;
        }//Eliminar

        /// <summary>
        /// Método para autenticar el usuario y otorgarle un Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar(string email, string password)
        {
            var temp = await _context.Usuario.FirstOrDefaultAsync(u =>
            (u.Email.Equals(email) && (u.Password.Equals(password))));

            if (temp == null)
            {
                return Unauthorized();
            }
            else
            {
                var autorizado = await _autorizacionServices.DevolverToken(temp);

                if (autorizado == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(autorizado);
                }
            }
        }//Autenticar

    }//class
}//namespace
