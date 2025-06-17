using College2Career.Models;

namespace College2Career.Repository
{
    public interface IOffersRepository
    {
        Task<bool> isOfferExist(int applicationId);
        Task newOffers(Offers offers);
    }
}
