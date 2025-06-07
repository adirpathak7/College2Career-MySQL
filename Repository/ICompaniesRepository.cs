using College2Career.Models;

namespace College2Career.Repository
{
    public interface ICompaniesRepository
    {
        public Task createCompanyProfile(Companies companies);
        public Task<Companies> getCompanyProfileByUsersId(int usersId);
        public Task<Companies> updateCompanyStatus(int companyId, string status, string statusReason);
        public Task<List<Companies>> getAllCompanies();
        public Task<List<Companies>> getCompaniesByPendingStatus();
        public Task<List<Companies>> getCompaniesByActivatedStatus();
        public Task<List<Companies>> getCompaniesByRejectedStatus();
        public Task<List<Companies>> getCompaniesByDeactivatedStatus();
        public Task<bool> getCompanyByActivatedStatus(int companyId);
        public Task<Companies> getCompanyProfileByCompanyId(int companyId);
    }
}
