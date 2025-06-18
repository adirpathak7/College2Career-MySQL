using College2Career.Data;
using College2Career.DTO;
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

        public async Task<Offers> updateOfferStatusAccepted(int offerId, OffersDTO offersDTO)
        {
            try
            {
                var offer = await c2CDBContext.Offers.FirstOrDefaultAsync(o => o.offerId == offerId);
                offer.status = offersDTO.status;
                c2CDBContext.Offers.Update(offer);
                await c2CDBContext.SaveChangesAsync();
                return offer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OffersRepository in method updateOfferStatusAccepted: {ex.Message}");
                throw;
            }
        }

        public async Task<Offers> updateOfferStatusRejected(int offerId, OffersDTO offersDTO)
        {
            try
            {
                var offer = await c2CDBContext.Offers.FirstOrDefaultAsync(o => o.offerId == offerId);
                offer.status = offersDTO.status;
                offer.reason = offersDTO.reason;
                c2CDBContext.Offers.Update(offer);
                c2CDBContext.SaveChanges();
                return offer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OffersRepository in method updateOfferStatusRejected: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Offers>> getAllOffersByStudentId(int studentId)
        {
            try
            {
                var offers = await c2CDBContext.Offers
                        .Include(o => o.Applications)
                        .ThenInclude(a => a.Students)
                        .Include(o => o.Applications)
                        .ThenInclude(a => a.Vacancies)
                        .ThenInclude(v => v.Companies)
                        .ToListAsync();

                return offers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OffersRepository in method getAllOffersByStudentId: {ex.Message}");
                throw;
            }
        }

    }
}
