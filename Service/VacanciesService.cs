using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class VacanciesService : IVacanciesService
    {
        private readonly IVacanciesRepository vacanciesRepository;
        private readonly ICompaniesRepository companiesRepository;
        public VacanciesService(IVacanciesRepository vacanciesRepository)
        {
            this.vacanciesRepository = vacanciesRepository;
        }
        public async Task<ServiceResponse<string>> createVacancies(VacanciesDTO vacanciesDTO, int companyId)
        {
            try
            {
                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

                var activatedStatus = companiesRepository.getCompanyByActivatedStatus(companyId);

                if (activatedStatus.Result == false)
                {
                    serviceResponse.data = "0";
                    serviceResponse.message = "Your company hasn't been approved yet!";
                    serviceResponse.status = false;
                }

                var newVacancy = new Vacancies
                {
                    title = vacanciesDTO.title,
                    description = vacanciesDTO.description,
                    eligibility_criteria = vacanciesDTO.eligibility_criteria,
                    totalVacancy = vacanciesDTO.totalVacancy,
                    timing = vacanciesDTO.timing,
                    package = vacanciesDTO.package,
                    type = vacanciesDTO.type,
                    locationType = vacanciesDTO.locationType
                };
                await vacanciesRepository.createVacancies(newVacancy);

                serviceResponse.data = "1";
                serviceResponse.message = "Vacancy created successfully!";
                serviceResponse.status = true;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in createVacancies method: " + ex.Message);
                throw;
            }

        }
    }
}
