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
                    annualPackage = vacanciesDTO.annualPackage,
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

        public async Task<ServiceResponse<List<VacanciesDTO>>> getVacanciesByCompanyId(int usersId)
        {
            try
            {
                var response = new ServiceResponse<List<VacanciesDTO>>();

                var company = await companiesRepository.getCompanyProfileByUsersId(usersId);
                if (company == null)
                {
                    response.data = null;
                    response.message = "Company not found for this user.";
                    response.status = false;
                    return response;
                }

                int companyId = company.companyId;

                var vacancies = await vacanciesRepository.getVacanciesByCompanyId(companyId);
                if (vacancies == null || !vacancies.Any())
                {
                    response.data = null;
                    response.message = "No vacancies found.";
                    response.status = false;
                    return response;
                }

                var vacanciesDTOList = vacancies.Select(v => new VacanciesDTO
                {
                    vacancyId = v.vacancyId,
                    title = v.title,
                    description = v.description,
                    eligibility_criteria = v.eligibility_criteria,
                    totalVacancy = v.totalVacancy,
                    timing = v.timing,
                    annualPackage = v.annualPackage,
                    type = v.type,
                    locationType = v.locationType,
                    status = v.status,
                    companyId = v.companyId
                }).ToList();

                response.data = vacanciesDTOList;
                response.status = true;
                response.message = "Vacancies fetched successfully.";
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in service: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> updateCompanyVacanciesByVacancyId(int vacancyId, int companyId, VacanciesDTO vacanciesDTO)
        {
            try
            {
                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

                var existVacancy = await vacanciesRepository.getVacancyByVacancyId(vacancyId);

                if (existVacancy == null)
                {
                    serviceResponse.data = "0";
                    serviceResponse.message = "Vacancy not found!";
                    serviceResponse.status = false;
                    return serviceResponse;
                }

                if (existVacancy.companyId != companyId)
                {
                    serviceResponse.data = "0";
                    serviceResponse.message = "Unauthorized! Vacancy doesn't belong to your company.";
                    serviceResponse.status = false;
                    return serviceResponse;
                }

                if (existVacancy.status == "hired")
                {
                    serviceResponse.data = "0";
                    serviceResponse.message = "Vacancy is already hired!";
                    serviceResponse.status = false;
                    return serviceResponse;
                }

                existVacancy.title = vacanciesDTO.title;
                existVacancy.description = vacanciesDTO.description;
                existVacancy.eligibility_criteria = vacanciesDTO.eligibility_criteria;
                existVacancy.totalVacancy = vacanciesDTO.totalVacancy;
                existVacancy.timing = vacanciesDTO.timing;
                existVacancy.annualPackage = vacanciesDTO.annualPackage;
                existVacancy.type = vacanciesDTO.type;
                existVacancy.locationType = vacanciesDTO.locationType;
                existVacancy.status = vacanciesDTO.status;

                await vacanciesRepository.updateCompanyVacanciesByVacancyId(existVacancy);

                serviceResponse.data = "1";
                serviceResponse.message = "Vacancy updated successfully!";
                serviceResponse.status = true;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in updateCompanyVacanciesByVacancyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<VacanciesDTO>>> getAllVacancies()
        {
            try
            {
                var serviceResponse = new ServiceResponse<List<VacanciesDTO>>();

                var allVacancies = await vacanciesRepository.getAllVacancies();

                if (allVacancies == null || !allVacancies.Any())
                {
                    serviceResponse.data = null;
                    serviceResponse.message = "No companies found.";
                    serviceResponse.status = false;
                }
                else
                {
                    var vacanciesDTO = allVacancies.Select(v => new VacanciesDTO
                    {
                        vacancyId = v.vacancyId,
                        title = v.title,
                        description = v.description,
                        timing = v.timing,
                        companyId = v.companyId,
                        eligibility_criteria = v.eligibility_criteria,
                        locationType = v.locationType,
                        annualPackage = v.annualPackage,
                        status = v.status,
                        totalVacancy = v.totalVacancy,
                        type = v.type,
                        companyName = v.Companies?.companyName,
                        email = v.Companies?.Users?.email,
                        contactNumber = v.Companies?.contactNumber,
                        industry = v.Companies?.industry,
                        employeeSize = v.Companies?.employeeSize
                    }).ToList();

                    serviceResponse.data = vacanciesDTO;
                    serviceResponse.message = "All vacancies fetched.";
                    serviceResponse.status = true;
                }
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in getAllVacancies method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<VacanciesDTO>>> getHiringVacanciesByCompanyId(int usersId)
        {
            try
            {
                var response = new ServiceResponse<List<VacanciesDTO>>();

                var company = await companiesRepository.getCompanyProfileByUsersId(usersId);

                if (company == null)
                {
                    response.data = null;
                    response.message = "Company not found for this user.";
                    response.status = false;
                    return response;
                }

                var companyId = company.companyId;

                var hiringVacancies = await vacanciesRepository.getHiringVacanciesByCompanyId(companyId);

                if (hiringVacancies == null || !hiringVacancies.Any())
                {
                    response.data = null;
                    response.message = "No hiring vacancies found for this company.";
                    response.status = false;
                    return response;
                }
                else
                {
                    var vacanciesDTO = hiringVacancies.Select(vh => new VacanciesDTO
                    {
                        vacancyId = vh.vacancyId,
                        title = vh.title,
                        description = vh.description,
                        eligibility_criteria = vh.eligibility_criteria,
                        totalVacancy = vh.totalVacancy,
                        timing = vh.timing,
                        annualPackage = vh.annualPackage,
                        type = vh.type,
                        locationType = vh.locationType,
                        status = vh.status,
                        companyId = vh.companyId,
                        createdAt = vh.createdAt,
                    }).ToList();
                    response.data = vacanciesDTO;
                    response.message = "Hiring vacancies fetched successfully.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in getHiringVacanciesByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<VacanciesDTO>>> getHiredVacanciesByCompanyId(int usersId)
        {
            try
            {
                var response = new ServiceResponse<List<VacanciesDTO>>();

                var company = await companiesRepository.getCompanyProfileByUsersId(usersId);

                if (company == null)
                {
                    response.data = null;
                    response.message = "Company not found for this user.";
                    response.status = false;
                    return response;
                }

                var companyId = company.companyId;

                var hiredVacancies = await vacanciesRepository.getHiredVacanciesByCompanyId(companyId);

                if (hiredVacancies == null || !hiredVacancies.Any())
                {
                    response.data = null;
                    response.message = "No hired vacancies found for this company.";
                    response.status = false;
                    return response;
                }
                else
                {
                    var vacanciesDTO = hiredVacancies.Select(vh => new VacanciesDTO
                    {
                        vacancyId = vh.vacancyId,
                        title = vh.title,
                        description = vh.description,
                        eligibility_criteria = vh.eligibility_criteria,
                        totalVacancy = vh.totalVacancy,
                        timing = vh.timing,
                        annualPackage = vh.annualPackage,
                        type = vh.type,
                        locationType = vh.locationType,
                        status = vh.status,
                        companyId = vh.companyId
                    }).ToList();
                    response.data = vacanciesDTO;
                    response.message = "Hired vacancies fetched successfully.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in getHiredVacanciesByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<VacanciesDTO>>> getHiringVacancies()
        {
            try
            {
                var serviceResponse = new ServiceResponse<List<VacanciesDTO>>();

                var allHiringVacancies = await vacanciesRepository.getHiringVacancies();

                if (allHiringVacancies == null || !allHiringVacancies.Any())
                {
                    serviceResponse.data = null;
                    serviceResponse.message = "No hiring companies found.";
                    serviceResponse.status = false;
                }
                else
                {
                    var vacanciesDTO = allHiringVacancies.Select(v => new VacanciesDTO
                    {
                        vacancyId = v.vacancyId,
                        title = v.title,
                        description = v.description,
                        timing = v.timing,
                        companyId = v.companyId,
                        eligibility_criteria = v.eligibility_criteria,
                        locationType = v.locationType,
                        annualPackage = v.annualPackage,
                        status = v.status,
                        totalVacancy = v.totalVacancy,
                        type = v.type,
                        companyName = v.Companies?.companyName,
                        email = v.Companies?.Users?.email,
                        contactNumber = v.Companies?.contactNumber,
                        industry = v.Companies?.industry,
                        employeeSize = v.Companies?.employeeSize
                    }).ToList();

                    serviceResponse.data = vacanciesDTO;
                    serviceResponse.message = "Hiring vacancies fetched.";
                    serviceResponse.status = true;
                }
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in getHiringVacancies method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<VacanciesDTO>>> getHiredVacancies()
        {
            try
            {
                var serviceResponse = new ServiceResponse<List<VacanciesDTO>>();

                var allHiredVacancies = await vacanciesRepository.getHiredVacancies();

                if (allHiredVacancies == null || !allHiredVacancies.Any())
                {
                    serviceResponse.data = null;
                    serviceResponse.message = "No hired companies found.";
                    serviceResponse.status = false;
                }
                else
                {
                    var vacanciesDTO = allHiredVacancies.Select(v => new VacanciesDTO
                    {
                        vacancyId = v.vacancyId,
                        title = v.title,
                        description = v.description,
                        timing = v.timing,
                        companyId = v.companyId,
                        eligibility_criteria = v.eligibility_criteria,
                        locationType = v.locationType,
                        annualPackage = v.annualPackage,
                        status = v.status,
                        totalVacancy = v.totalVacancy,
                        type = v.type,
                        companyName = v.Companies?.companyName,
                        email = v.Companies?.Users?.email,
                        contactNumber = v.Companies?.contactNumber,
                        industry = v.Companies?.industry,
                        employeeSize = v.Companies?.employeeSize
                    }).ToList();

                    serviceResponse.data = vacanciesDTO;
                    serviceResponse.message = "Hired vacancies fetched.";
                    serviceResponse.status = true;
                }
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in getHiredVacancies method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> updateVacanciesStatus(int vacancyId, int usersId, string status)
        {
            try
            {
                var response = new ServiceResponse<string>();
                var existCompany = await companiesRepository.getCompanyProfileByUsersId(usersId);

                if (existCompany == null || existCompany.status != "activated")
                {
                    response.data = "0";
                    response.message = "Your company hasn't been approved yet!";
                    response.status = false;
                    return response;
                }

                var existVacancy = await vacanciesRepository.getVacancyByVacancyId(vacancyId);

                if (existVacancy == null)
                {
                    response.data = "0";
                    response.message = "Vacancy not found!";
                    response.status = false;
                    return response;
                }

                if (existCompany.companyId != existVacancy.companyId)
                {
                    response.data = "0";
                    response.message = "Unauthorized! Vacancy doesn't belong to your company.";
                    response.status = false;
                    return response;
                }

                if (status == null)
                {
                    response.data = "0";
                    response.message = "Invalid status value!";
                    response.status = false;
                    return response;
                }

                existVacancy.status = status;

                await vacanciesRepository.updateVacanciesStatus(existVacancy);

                response.data = "1";
                response.message = "Vacancy status updated successfully!";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies service in updateVacanciesStatus method: " + ex.Message);
                throw;
            }
        }
    }
}