using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Paquete
    {
        [Key]
        public int Id { get; set; }
        public string Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public decimal Costo_Persona { get; set; }
        public int Adelanto { get; set; }
        public int Terminos_Pago { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
