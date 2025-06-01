using College2Career.Data;
using College2Career.DTO;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;

namespace College2Career.Repository
{
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly C2CDBContext c2CDBContext;
        public ApplicationsRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }


        public async Task<bool> alreadyAppliedForVacancy(int studentId, int vacancyId)
        {
            try
            {
                var checkApplication = await c2CDBContext.Applications.FirstOrDefaultAsync(a => a.studentId == studentId && a.vacancyId == vacancyId);

                if (checkApplication == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in ApplicationsRepository in alreadyAppliedForVacancy method: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> isOfferAccepted(int studentId)
        {
            try
            {
                var checkApplication = await c2CDBContext.Applications.Where(a=> a.status == "offerAccepted").ToListAsync();
                if (checkApplication == null || checkApplication.Count == 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in ApplicationsRepository in isOfferAccepted method: " + ex.Message);
                throw;
            }
        }

        public async Task newApplications(Applications applications)
        {
            try
            {
                await c2CDBContext.AddAsync(applications);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in ApplicationsRepository in newApplications method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Applications>> getAllAppliedApplicationsByCompanyId(int companyId)
        {
            try
            {
                var allApplications = await c2CDBContext.Applications
                    .Include(a => a.Students)
                    .Include(a => a.Vacancies)
                    //.Include(a => a.Vacancies.Companies)
                    .Include(a => a.Students.Users)
                    //.Include(a => a.Vacancies.Companies.Users)
                    .Where(a => a.Vacancies.companyId == companyId)
                    .ToListAsync();

                return allApplications;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in ApplicationsRepository in getAllAppliedApplications method: " + ex.Message);
                throw;
            }
        }
    }
}
