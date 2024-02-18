using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Cliente
    {
        [Key]
        public string Cedula { get; set; }
        public string Tipo_Cedula { get; set; }
        public string Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
