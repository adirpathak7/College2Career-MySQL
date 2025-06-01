using College2Career.Data;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Vacancies>> getVacanciesByCompanyId(int companyId)
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies.Where(v => v.companyId == companyId).ToListAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getVacanciesByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<Vacancies> getVacancyByVacancyId(int vacancyId)
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies.FindAsync(vacancyId);
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getVacancyByVacancyId method: " + ex.Message);
                throw;
            }
        }

        public async Task updateCompanyVacanciesByVacancyId(Vacancies vacancies)
        {
            try
            {
                c2CDBContext.Update(vacancies);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in updateCompanyVacanciesByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Vacancies>> getAllVacancies()
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies.Include(c => c.Companies).ToListAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getAllVacancies method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Vacancies>> getHiringVacanciesByCompanyId(int companyId)
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies
                    .Where(v => v.companyId == companyId && v.status == "hiring")
                    .Include(v => v.Companies)
                    .ToListAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getHiringVacanciesByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Vacancies>> getHiredVacanciesByCompanyId(int companyId)
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies
                    .Where(v => v.companyId == companyId && v.status == "hired")
                    .Include(v => v.Companies)
                    .ToListAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getHiredVacanciesByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Vacancies>> getHiringVacancies()
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies.Where(v => v.status == "hiring").Include(v => v.Companies).ToListAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getHiringVacancies method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Vacancies>> getHiredVacancies()
        {
            try
            {
                var vacancies = await c2CDBContext.Vacancies.Where(v => v.status == "hired").Include(v => v.Companies).ToListAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in getHiredVacancies method: " + ex.Message);
                throw;
            }
        }

        public async Task updateVacanciesStatus(Vacancies vacancies)
        {
            try
            {
                c2CDBContext.Vacancies.Update(vacancies);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in vacancies repository in updateVacanciesStatus method: " + ex.Message);
                throw;
            }
        }
    }
}
