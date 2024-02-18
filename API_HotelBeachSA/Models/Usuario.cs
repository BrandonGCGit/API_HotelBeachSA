using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Usuario
    {
        [Key]
        public string Cedula { get; set; }
        public int Id_Rol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
