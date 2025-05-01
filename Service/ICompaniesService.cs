using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface ICompaniesService
    {
        Task<ServiceResponse<string>> createCompanyProfile(CompaniesDTO companiesDTO, int usersId);
        Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesProfileByUsersId(int usersId);
        Task<ServiceResponse<List<CompaniesDTO>>> getPendingStatus();
        Task<ServiceResponse<string>> updateCompanyStatus(int companyId, string status, string statusReason);
    }
}
