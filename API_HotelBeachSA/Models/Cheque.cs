using System.ComponentModel.DataAnnotations;

namespace API_HotelBeachSA.Models
{
    public class Cheque
    {
        [Key]
        public int Id { get; set; }
        public string Moneda { get; set; }
        public decimal Cantidad { get; set; }
        public string Nombre_Banco { get; set; }
        public char Estado { get; set; }
    }
}
