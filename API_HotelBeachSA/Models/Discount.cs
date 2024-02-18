using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string Id_Usuario { get; set; }
        public int Descuento { get; set; }
        public int Noches { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
