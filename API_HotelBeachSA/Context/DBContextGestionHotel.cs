using Microsoft.EntityFrameworkCore;
using API_HotelBeachSA.Models;

namespace API_HotelBeachSA.Context
{
    public class DBContextGestionHotel : DbContext
    {

        public DBContextGestionHotel(DbContextOptions<DBContextGestionHotel> options) : base(options)
        {

        }

        //Cliente
        public DbSet<API_HotelBeachSA.Models.Cliente> Cliente { get; set; }
        //Paquete
        public DbSet<API_HotelBeachSA.Models.Paquete> Paquete { get; set; }
        //Usuario
        public DbSet<API_HotelBeachSA.Models.Usuario> Usuario { get; set; }
        //Descuento
        public DbSet<API_HotelBeachSA.Models.Discount> Descuento { get; set; }
        //Reservacion
        public DbSet<API_HotelBeachSA.Models.Reservacion> Reservacion { get; set; }
        //Pago
        public DbSet<API_HotelBeachSA.Models.Pago> Pago { get; set; }
        //Cheque
        public DbSet<API_HotelBeachSA.Models.Cheque> Cheque { get; set; }
        //Rol
        public DbSet<API_HotelBeachSA.Models.Rol> Rol { get; set; }

    }
}
