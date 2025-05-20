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

        public async Task<Companies> getCompanyProfileByUsersId(int usersId)
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

        public async Task<List<Companies>> getAllCompanies()
        {
            try
            {
                var allCompanies = await c2CDBContext.Companies.Include(u => u.Users).ToListAsync();
                return allCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getAllCompanies method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompaniesByPendingStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Include(u => u.Users).Where(c => c.status == "pending").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompaniesByPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompaniesByActivatedStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Include(u => u.Users).Where(c => c.status == "activated").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompaniesByActivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompaniesByRejectedStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Include(u => u.Users).Where(c => c.status == "rejected").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompaniesByRejectedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Companies>> getCompaniesByDeactivatedStatus()
        {
            try
            {
                var pendingCompanies = await c2CDBContext.Companies.Include(u => u.Users).Where(c => c.status == "deactivated").ToListAsync();
                return pendingCompanies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompaniesByDeactivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> getCompanyByActivatedStatus(int companyId)
        {
            try
            {
                var existCompany = await c2CDBContext.Companies.FirstOrDefaultAsync(c => c.companyId == companyId);
                if (existCompany == null) return false;
                if (existCompany.status == "activated") return true;
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in company repository in getCompanyByActivatedStatus method: " + ex.Message);
                throw;
            }
        }
    }
}
