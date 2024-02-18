using API_HotelBeachSA.Models;
using API_HotelBeachSA.Models.Custom;

namespace API_HotelBeachSA.Services
{
    public interface IAutorizacionServices
    {

        Task<AutorizacionResponse> DevolverToken(Usuario autorizacion);

    }
}
