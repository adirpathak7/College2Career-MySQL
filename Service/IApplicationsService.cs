using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Repository;

namespace College2Career.Service
{
    public interface IApplicationsService
    {
        Task<ServiceResponse<string>> newApplications(ApplicationsDTO applicationsDTO, int usersId);
        Task<ServiceResponse<List<VacanciesAppliedStudentsDTO>>> getAllAppliedApplicationsByCompanyId(int usersId);
        Task<ServiceResponse<string>> updateApplicationsStatusByCompany(int applicationId, UpdateApplicationStatusDTO updateApplicationStatusDTO);
    }
}
