using API_HotelBeachSA.Models;
using API_HotelBeachSA.Models.Custom;

//Librerias para implementar JWT
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

//Librerias para implementar el ORM
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_HotelBeachSA.Context;


namespace API_HotelBeachSA.Services
{

    public class AutorizacionServices : IAutorizacionServices
    {

        //Variable para usar el archivo de appsettings.json
        private readonly IConfiguration _configuration;
        //Variable para usar las funciones ORM
        private readonly DBContextGestionHotel _context;

        public AutorizacionServices(IConfiguration configuration, DBContextGestionHotel context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<AutorizacionResponse> DevolverToken(Usuario autorizacion)
        {
            var temp = await _context.Usuario.FirstOrDefaultAsync(u =>
            (u.Email.Equals(autorizacion.Email)) &&
            (u.Password.Equals(autorizacion.Password)));

            if (temp == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }

            string tokenCreado = GenerarToken(autorizacion.Cedula.ToString());

            return new AutorizacionResponse()
            {
                Token = tokenCreado,
                Resultado = true,
                Msg = "Ok"
            };
        }

        private string GenerarToken(string idUsuario)
        {
            //Se realiza lectura de la key almacenada dentro del archivo de confi de JSON
            var key = _configuration.GetValue<string>("JwtSettings:Key");

            //Se convierte la key en un vector de bytes
            var keyBytes = Encoding.ASCII.GetBytes(key);

            //Identidad que realiza el reclamo para la autorizacion
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            //Credenciales del token
            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),// llave de seguridad
                SecurityAlgorithms.HmacSha256Signature);//Metodo de cifrado

            //Creacion descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,//Identidad
                Expires = DateTime.UtcNow.AddMinutes(1),//Tiempo de vida del token
                SigningCredentials = credencialesToken //Credenciales del Token
            };

            var tokenHandler = new JwtSecurityTokenHandler();//Se crea el TokenHandler para construir el token

            //Se Escribe el token
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);//Se crea el token

            //Se devuelve el Token
            var tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;

        }
    }
}
