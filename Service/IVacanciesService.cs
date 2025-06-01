using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface IVacanciesService
    {
        Task<ServiceResponse<string>> createVacancies(VacanciesDTO vacanciesDTO, int companyId);
        Task<ServiceResponse<List<VacanciesDTO>>> getVacanciesByCompanyId(int usersId);
        Task<ServiceResponse<string>> updateCompanyVacanciesByVacancyId(int vacancyId, int companyId, VacanciesDTO vacanciesDTO);
        Task<ServiceResponse<List<VacanciesDTO>>> getHiringVacanciesByCompanyId(int companyId);
        Task<ServiceResponse<List<VacanciesDTO>>> getHiredVacanciesByCompanyId(int companyId);
        Task<ServiceResponse<List<VacanciesDTO>>> getAllVacancies();
        Task<ServiceResponse<List<VacanciesDTO>>> getHiringVacancies();
        Task<ServiceResponse<List<VacanciesDTO>>> getHiredVacancies();
        Task<ServiceResponse<string>> updateVacanciesStatus(int vacancyId, int usersId, string status);
    }
}
