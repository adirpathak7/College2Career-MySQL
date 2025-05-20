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
        public VacanciesService(IVacanciesRepository vacanciesRepository, ICompaniesRepository companiesRepository)
        {
            this.vacanciesRepository = vacanciesRepository;
            this.companiesRepository = companiesRepository;
        }
        public async Task<ServiceResponse<string>> createVacancies(VacanciesDTO vacanciesDTO, int usersId)
        {
            try
            {
                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

                var existCompany = await companiesRepository.getCompanyProfileByUsersId(usersId);

                if (existCompany == null || existCompany.status != "activated")
                {
                    serviceResponse.data = "0";
                    serviceResponse.message = "Your company hasn't been approved yet!";
                    serviceResponse.status = false;
                    return serviceResponse;
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
                    locationType = vacanciesDTO.locationType,
                    companyId = existCompany.companyId
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
