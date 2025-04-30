using College2Career.Models;

namespace College2Career.Repository
{
    public interface ICompaniesRepository
    {
        public Task createCompanyProfile(Companies companies);

        public Task<Companies> getCompanyByUserId(int usersId);

        public Task<Companies> getCompaniesProfileByUsersId(int usersId);
        public Task<List<Companies>> getPendingStatus();
        public Task activeCompanyStatus(int companyId);
        public Task rejectCompanyStatus(int companyId);
    }
}
