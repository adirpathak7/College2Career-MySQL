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


        public async Task<bool> alreadyAppliedForVacancy(int studentId)
        {
            try
            {
                var checkApplication = await c2CDBContext.Applications.FirstOrDefaultAsync(a => a.studentId == studentId);

                if (checkApplication == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in applications repository in alreadyAppliedForVacancy method: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> isOfferAccepted(int studentId)
        {
            try
            {
                var checkApplication = await c2CDBContext.Applications.Where(a => a.status == "offerAccepted").ToListAsync();
                if (checkApplication == null || checkApplication.Count == 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in applications repository in isOfferAccepted method: " + ex.Message);
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
                Console.WriteLine("ERROR in applications repository in newApplications method: " + ex.Message);
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
                Console.WriteLine("ERROR in applications repository in getAllAppliedApplications method: " + ex.Message);
                throw;
            }
        }

        public async Task<Applications> isApplicationsExist(int applicationId)
        {
            try
            {
                var checkApplication = await c2CDBContext.Applications.FindAsync(applicationId);
                return checkApplication;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in applications repository in isApplicationsExist method: " + ex.Message);
                throw;
            }
        }
        public async Task updateApplicationsStatusByCompany(Applications applications)
        {
            try
            {
                c2CDBContext.Applications.Update(applications);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in applications repository in updateApplicationsStatusByCompany method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Applications>> getAllAppliedApplicationsByStudentId(int studentId)
        {
            try
            {
                var allApplications = await c2CDBContext.Applications
                    .Include(a => a.Vacancies)
                    .Include(a => a.Vacancies.Companies)
                    .Include(a => a.Vacancies.Companies.Users)
                    .Where(a => a.studentId == studentId)
                    .ToListAsync();
                return allApplications;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in applications repository in getAllAppliedApplicationsByStudentId method: " + ex.Message);
                throw;
            }
        }

        public async Task<Applications> getApplicationDetailsById(int applicationId)
        {
            try
            {
                var existApplication = await c2CDBContext.Applications
                .Include(a => a.Students)
                    .ThenInclude(s => s.Users)
                .FirstOrDefaultAsync(a => a.applicationId == applicationId);
                return existApplication;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in applications repository in getAllAppliedApplicationsByStudentId method: " + ex.Message);
                throw;
            }
        }

        public async Task<Applications> updateStatusToOfferedByStudentId(int applicationId)
        {
            var existApplication = await c2CDBContext.Applications.FindAsync(applicationId);
            if (existApplication == null) return null;

            existApplication.status = "offered";
            c2CDBContext.Applications.Update(existApplication);
            await c2CDBContext.SaveChangesAsync();
            return existApplication;
        }

        public async Task<Applications> updateStatusToOfferAcceptedStudentId(int applicationId)
        {
            var existApplication = await c2CDBContext.Applications.FindAsync(applicationId);
            if (existApplication == null) return null;

            existApplication.status = "offerAccepted";
            c2CDBContext.Applications.Update(existApplication);
            await c2CDBContext.SaveChangesAsync();
            return existApplication;
        }

        public async Task<Applications> updateStatusToOfferRejectedStudentId(int applicationId, ApplicationsDTO applicationsDTO)
        {
            var existApplication = await c2CDBContext.Applications.FindAsync(applicationId);
            if (existApplication == null) return null;

            existApplication.status = "offerRejected";
            existApplication.reason = applicationsDTO.reason;
            c2CDBContext.Applications.Update(existApplication);
            await c2CDBContext.SaveChangesAsync();
            return existApplication;
        }
    }
}
