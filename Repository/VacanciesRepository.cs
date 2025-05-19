using College2Career.Data;
using College2Career.Models;
using Microsoft.VisualBasic;

namespace College2Career.Repository
{
    public class VacanciesRepository : IVacanciesRepository
    {
        private readonly C2CDBContext c2CDBContext;
        private readonly ICompaniesRepository companiesRepository;

        public VacanciesRepository(C2CDBContext c2CDBContext, ICompaniesRepository companiesRepository)
        {
            this.c2CDBContext = c2CDBContext;
            this.companiesRepository = companiesRepository;
        }

        public async Task createVacancies(Vacancies vacancies)
        {
            try
            {
                await c2CDBContext.AddAsync(vacancies);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in createVacancies method: " + ex.Message);
                throw;
            }
        }

    }
}
