using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface IOffersService
    {
        Task<ServiceResponse<string>> newOffers(OffersDTO offersDTO, int usersId);
    }
}
