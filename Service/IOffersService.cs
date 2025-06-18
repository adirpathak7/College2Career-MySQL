using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;

namespace College2Career.Service
{
    public interface IOffersService
    {
        Task<ServiceResponse<string>> newOffers(OffersDTO offersDTO, int usersId);
        Task<ServiceResponse<Offers>> updateOfferStatusAccepted(int offerId, OffersDTO offersDTO);
        Task<ServiceResponse<Offers>> updateOfferStatusRejected(int offerId, OffersDTO offersDTO);
        Task<ServiceResponse<List<OffersDTO>>> getAllOffersByStudentId(int studentId);
    }
}
