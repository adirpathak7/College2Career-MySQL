using College2Career.Data;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace College2Career.Repository
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly C2CDBContext c2CDBContext;

        public CompaniesRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }


        public async Task createCompanyProfile(Companies companies)
        {
            try
            {
                await c2CDBContext.Companies.AddAsync(companies);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in createCompanyProfile method: " + ex.Message);
                throw;
            }
        }

        public async Task<Companies> getCompaniesProfileByUsersId(int usersId)
        {
            try
            {
                var existCompany = await c2CDBContext.Companies.FirstOrDefaultAsync(c => c.usersId == usersId);
                return existCompany;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompaniesProfileByUsersId method: " + ex.Message);
                throw;
            }
        }

        public async Task<Companies> updateCompanyStatus(int companyId, string status, string statusReason)
        {
            try
            {
                var existCompany = await c2CDBContext.Companies
                    .Include(c => c.Users).FirstAsync(c => c.companyId == companyId);

                if (existCompany == null) return null;

                existCompany.status = status;
                existCompany.reasonOfStatus = statusReason;
                await c2CDBContext.SaveChangesAsync();

                return existCompany;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in updateCompanyStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompanyAllCompanies()
        {
            try
            {
                var allCompanies = await c2CDBContext.Companies.ToListAsync();
                return allCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompanyAllCompanies method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompanyByPendingStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Where(c => c.status == "pending").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompanyByPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompanyByActivatedStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Where(c => c.status == "activated").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompanyByActivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompanyByRejectedStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Where(c => c.status == "rejected").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompanyByRejectedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompanyByDeactivatedStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Where(c => c.status == "deactivated").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompanyByDeactivatedStatus method: " + ex.Message);
                throw;
            }
        }
    }
}
