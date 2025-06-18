using College2Career.DTO;
using College2Career.Models;

namespace College2Career.Repository
{
    public interface IOffersRepository
    {
        Task<bool> isOfferExist(int applicationId);
        Task newOffers(Offers offers);
        Task<Offers> updateOfferStatusAccepted(int offerId, OffersDTO offersDTO);
        Task<Offers> updateOfferStatusRejected(int offerId, OffersDTO offersDTO);
        Task<List<Offers>> getAllOffersByStudentId(int studentId);
    }
}
