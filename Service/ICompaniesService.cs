using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface ICompaniesService
    {
        Task<ServiceResponse<string>> createCompanyProfile(CompaniesDTO companiesDTO, int usersId);
        Task<ServiceResponse<List<CompaniesDTO>>> getCompanyProfileByUsersId(int usersId);
        Task<ServiceResponse<string>> updateCompanyStatus(int companyId, string status, string statusReason);
        Task<ServiceResponse<List<CompaniesDTO>>> getAllCompanies();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesByPendingStatus();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesByActivatedStatus();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesByRejectedStatus();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesByDeactivatedStatus();
        Task<CompanyDashboardStatsDTO> getCompanyDashboardStats(int usersId);

    }
}
