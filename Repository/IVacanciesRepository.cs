using College2Career.Models;

namespace College2Career.Repository
{
    public interface IVacanciesRepository
    {
        Task createVacancies(Vacancies vacancies);
        Task<List<Vacancies>> getVacanciesByCompanyId(int companyId);
        Task<Vacancies> getVacancyByVacancyId(int vacancyId);
        Task updateCompanyVacanciesByVacancyId(Vacancies vacancies);
        Task<List<Vacancies>> getHiringVacanciesByCompanyId(int companyId);
        Task<List<Vacancies>> getHiredVacanciesByCompanyId(int companyId);
        Task<List<Vacancies>> getAllVacancies();
        Task<List<Vacancies>> getHiringVacancies();
        Task<List<Vacancies>> getHiredVacancies();
        Task updateVacanciesStatus(Vacancies vacancies);
    }
}
