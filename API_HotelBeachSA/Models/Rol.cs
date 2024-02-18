using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Funciones { get; set; }
        public DateTime Fecha_Registro { get; set; }

    }
}
