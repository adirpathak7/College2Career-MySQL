using College2Career.Models;

namespace College2Career.Repository
{
    public interface ICompaniesRepository
    {
        public Task createCompanyProfile(Companies companies);
        public Task<Companies> getCompaniesProfileByUsersId(int usersId);
        public Task<Companies> updateCompanyStatus(int companyId, string status, string statusReason);
        public Task<List<Companies>> getCompanyAllCompanies();
        public Task<List<Companies>> getCompanyByPendingStatus();
        public Task<List<Companies>> getCompanyByActivatedStatus();
        public Task<List<Companies>> getCompanyByRejectedStatus();
        public Task<List<Companies>> getCompanyByDeactivatedStatus();

    }
}
