using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository companiesRepository;
        private readonly ICloudinaryService cloudinaryService;


        public CompaniesService(ICompaniesRepository companiesRepository, ICloudinaryService cloudinaryService)
        {
            this.companiesRepository = companiesRepository;
            this.cloudinaryService = cloudinaryService;
        }


        public async Task<ServiceResponse<string>> createCompanyProfile(CompaniesDTO companiesDTO, int usersId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var imageURL = await cloudinaryService.uploadImages(companiesDTO.profilePicture);

                if (imageURL == null)
                {
                    response.data = "0";
                    response.message = "Image upload failed.";
                    response.status = false;
                    return response;
                }

                if (usersId == null || usersId == 0)
                {
                    response.data = "0";
                    response.message = "Unauthorized! Please login again. ";
                    response.status = false;
                }

                var existingCompany = await companiesRepository.getCompanyByUserId(usersId);
                if (existingCompany != null)
                {
                    response.data = "0";
                    response.message = "Your profile is already exists!";
                    response.status = false;
                    return response;
                }

                var newCompany = new Companies
                {
                    usersId = usersId,
                    companyName = companiesDTO.companyName,
                    establishedDate = companiesDTO.establishedDate,
                    contactNumber = companiesDTO.contactNumber,
                    profilePicture = imageURL,
                    industry = companiesDTO.industry,
                    area = companiesDTO.area,
                    city = companiesDTO.city,
                    state = companiesDTO.state,
                    employeeSize = companiesDTO.employeeSize,
                    createdAt = DateTime.Now,
                };

                await companiesRepository.createCompanyProfile(newCompany);
                response.data = "1";
                response.message = "Profile created successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in createCompanyProfile method: " + ex.Message);
                throw;
            }
        }


        public async Task<ServiceResponse<List<CompaniesDTO>>> getCompaniesProfileByUsersId(int userId)
        {
            try
            {
                var response = new ServiceResponse<List<CompaniesDTO>>();

                var existCompany = await companiesRepository.getCompaniesProfileByUsersId(userId);
                Console.WriteLine("existCompany.companyName: " + existCompany.companyName);
                if (existCompany == null)
                {
                    response.data = new List<CompaniesDTO>();
                    response.message = "No company profile found.";
                    response.status = true;
                }
                else
                {
                    var companyDTO = new CompaniesDTO
                    {
                        companyId = existCompany.companyId,
                        companyName = existCompany.companyName,
                        area = existCompany.area,
                        city = existCompany.city,
                        state = existCompany.state,
                        establishedDate = existCompany.establishedDate,
                        contactNumber = existCompany.contactNumber,
                        profilePictureURL = existCompany.profilePicture,
                        industry = existCompany.industry,
                        employeeSize = existCompany.employeeSize,
                        status = existCompany.status,
                        reasonOfStatus = existCompany.reasonOfStatus,
                        createdAt = existCompany.createdAt
                    };

                    response.data = new List<CompaniesDTO> { companyDTO };
                    response.message = "Company profile found.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in getCompaniesProfileByUsersId method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<CompaniesDTO>>> getPendingStatus()
        {
            try
            {
                var response = new ServiceResponse<List<CompaniesDTO>>();

                var pendingCompanies = await companiesRepository.getPendingStatus();

                if (pendingCompanies == null || !pendingCompanies.Any())
                {
                    response.data = new List<CompaniesDTO>();
                    response.message = "No pending companies found.";
                    response.status = false;
                }
                else
                {
                    var pendingDTOs = pendingCompanies.Select(c => new CompaniesDTO
                    {
                        companyId = c.companyId,
                        companyName = c.companyName,
                        area = c.area,
                        city = c.city,
                        state = c.state,
                        establishedDate = c.establishedDate,
                        contactNumber = c.contactNumber,
                        profilePictureURL = c.profilePicture,
                        industry = c.industry,
                        employeeSize = c.employeeSize,
                        status = c.status,
                        reasonOfStatus = c.reasonOfStatus,
                        usersId = c.usersId
                    }).ToList();

                    response.data = pendingDTOs;
                    response.message = "Pending companies found.";
                    response.status = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in getPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> activeCompanyStatus(int companyId)
        {
            try
            {
                ServiceResponse<string> response = new ServiceResponse<string>();

                var existCompany = companiesRepository.activeCompanyStatus(companyId);

                if (existCompany == null)
                {
                    response.data = "0";
                    response.message = "No company found.";
                    response.status = false;
                }

                response.data = "1";
                response.message = "Company status activeted.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in activeCompanyStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> rejectCompanyStatus(int companyId)
        {
            try
            {
                ServiceResponse<string> response = new ServiceResponse<string>();
                var existCompany = companiesRepository.rejectCompanyStatus(companyId);

                if(existCompany == null)
                {
                    response.data = "0";
                    response.message = "No company found.";
                    response.status = false;
                }

                response.data = "1";
                response.message = "Company status rejected.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in rejectCompanyStatus method: " + ex.Message);
                throw;
            }
        }
    }
}
