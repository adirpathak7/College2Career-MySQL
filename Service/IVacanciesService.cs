using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface IVacanciesService
    {
        Task<ServiceResponse<string>> createVacancies(VacanciesDTO vacanciesDTO, int companyId);
    }
}
