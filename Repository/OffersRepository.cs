using College2Career.Data;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;

namespace College2Career.Repository
{
    public class OffersRepository : IOffersRepository
    {
        private readonly C2CDBContext c2CDBContext;
        public OffersRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }

        public async Task<bool> isOfferExist(int applicationId)
        {
            try
            {
                var existingOffer = await c2CDBContext.Offers.AnyAsync(o => o.applicationId == applicationId);
                return existingOffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OffersRepository in method isOfferExist: {ex.Message}");
                throw;
            }
        }

        public async Task newOffers(Offers offers)
        {
            try
            {
                await c2CDBContext.Offers.AddAsync(offers);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OffersRepository in method newOffers: {ex.Message}");
                throw;
            }
        }
    }
}
