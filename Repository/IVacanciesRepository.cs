using College2Career.Models;

namespace College2Career.Repository
{
    public interface IVacanciesRepository
    {
        Task createVacancies(Vacancies vacancies);
        //Task<Vacancies> getVacanciesByCompanyId(int companyId);
    }
}
