using College2Career.Models;

namespace College2Career.Repository
{
    public interface ICompaniesRepository
    {
        public Task createCompanyProfile(Companies companies);
        public Task<Companies> getCompaniesProfileByUsersId(int usersId);
        public Task<List<Companies>> getCompanyByPendingStatus();
        public Task<Companies> updateCompanyStatus(int companyId, string status, string statusReason);
    }
}
