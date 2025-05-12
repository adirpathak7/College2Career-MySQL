using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;
using Microsoft.VisualBasic;

namespace College2Career.Service
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository companiesRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IEmailService emailService;

        public CompaniesService(ICompaniesRepository companiesRepository, ICloudinaryService cloudinaryService, IEmailService emailService)
        {
            this.companiesRepository = companiesRepository;
            this.cloudinaryService = cloudinaryService;
            this.emailService = emailService;
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

                if (usersId == 0)
                {
                    response.data = "0";
                    response.message = "Unauthorized! Please login again. ";
                    response.status = false;
                }

                var existingCompany = await companiesRepository.getCompaniesProfileByUsersId(usersId);
                
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
                if (existCompany == null)
                {
                    response.data = new List<CompaniesDTO>();
                    response.message = "No company profile found.";
                    response.status = true;
                }
                else
                {
                    var companyProfile = new CompaniesDTO
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

                    response.data = new List<CompaniesDTO> { companyProfile };
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

        public async Task<ServiceResponse<List<CompaniesDTO>>> getCompanyByPendingStatus()
        {
            try
            {
                var response = new ServiceResponse<List<CompaniesDTO>>();

                var pendingCompanies = await companiesRepository.getCompanyByPendingStatus();

                if (pendingCompanies == null || !pendingCompanies.Any())
                {
                    response.data = new List<CompaniesDTO>();
                    response.message = "No pending companies found.";
                    response.status = false;
                }
                else
                {
                    var pendingCompany = pendingCompanies.Select(c => new CompaniesDTO
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

                    response.data = pendingCompany;
                    response.message = "Pending companies found.";
                    response.status = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in getCompanyByPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> updateCompanyStatus(int companyId, string status, string statusReason)
        {
            try
            {
                ServiceResponse<string> response = new ServiceResponse<string>();

                var existCompany = await companiesRepository.updateCompanyStatus(companyId, status, statusReason);

                if (existCompany == null)
                {
                    response.data = "0";
                    response.message = "No company found.";
                    response.status = false;
                }
                else
                {
                    int roleIdIs = (int)(existCompany.Users.roleId);
                    Console.WriteLine(roleIdIs);
                    if (status == "activated")
                    {
                        string subject = "<b>Profile Verification</b>";
                        string body = emailService.createActivetedEmailBody(existCompany.companyName, (int)(existCompany.Users.roleId));
                        await emailService.sendEmail(existCompany.Users.email, subject, body);
                    }
                    else if (status == "rejected")
                    {
                        string subject = "<b>Profile Verification</b>";
                        string body = emailService.createRejectedEmailBody(existCompany.companyName, existCompany.reasonOfStatus, (int)(existCompany.Users.roleId));
                        await emailService.sendEmail(existCompany.Users.email, subject, body);
                    }
                    else if (status == "deactivated")
                    {
                        string subject = "<b>Profile Verification</b>";
                        string body = emailService.createDeactivatedEmailBody(existCompany.companyName, existCompany.reasonOfStatus, (int)(existCompany.Users.roleId));
                        await emailService.sendEmail(existCompany.Users.email, subject, body);
                    }

                    response.data = "1";
                    response.message = "Company status updated.";
                    response.status = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company service in updateCompanyStatus method: " + ex.Message);
                throw;
            }
        }

    }
}
