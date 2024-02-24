using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Reservacion
    {

        [Key]
        public int Id { get; set; }
        public string Id_Cliente { get; set; }
        public int Id_Paquete { get; set; }
        public int Id_Descuento { get; set; }
        public int Huespedes { get; set; }
        public decimal Total { get; set; }
        public int Noches { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Salida { get; set; }
        public DateTime Fecha_Registro { get; set; }


    }
}
