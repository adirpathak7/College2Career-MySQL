using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface ICompaniesService
    {
        Task<ServiceResponse<string>> createCompanyProfile(CompaniesDTO companiesDTO, int usersId);
        Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesProfileByUsersId(int usersId);
        Task<ServiceResponse<string>> updateCompanyStatus(int companyId, string status, string statusReason);
        Task<ServiceResponse<List<CompaniesDTO>>> getCompanyAllCompanies();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompanyByPendingStatus();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompanyByActivatedStatus();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompanyByRejectedStatus();
        Task<ServiceResponse<List<CompaniesDTO>>> getCompanyByDeactivatedStatus();
    }
}
