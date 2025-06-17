using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class OffersService : IOffersService
    {
        private readonly IOffersRepository offersRepository;
        private readonly IEmailService emailService;
        private readonly ICompaniesRepository companiesRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IApplicationsRepository applicationsRepository;
        public OffersService(IOffersRepository offersRepository, IEmailService emailService, ICompaniesRepository companiesRepository, ICloudinaryService cloudinaryService, IApplicationsRepository applicationsRepository)
        {
            this.offersRepository = offersRepository;
            this.emailService = emailService;
            this.companiesRepository = companiesRepository;
            this.cloudinaryService = cloudinaryService;
            this.applicationsRepository = applicationsRepository;
        }

        public async Task<ServiceResponse<string>> newOffers(OffersDTO offersDTO, int usersId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var offerLetterPDF = await cloudinaryService.uploadImages(offersDTO.offerLetter);

                if (offerLetterPDF == null)
                {
                    response.data = null;
                    response.message = "Offer letter upload failed!";
                    response.status = false;
                    return response;
                }

                var existUser = await companiesRepository.getCompanyProfileByUsersId(usersId);
                if (existUser == null)
                {
                    response.data = null;
                    response.message = "Company profile not found.";
                    response.status = false;
                    return response;
                }

                var offerExists = await offersRepository.isOfferExist((int)offersDTO.applicationId);
                if (offerExists)
                {
                    response.data = null;
                    response.message = "Offer letter already exists!";
                    response.status = false;
                    return response;
                }

                var companyId = existUser.companyId;
                if (companyId == null)
                {
                    response.data = null;
                    response.message = "Company not found for this user!";
                    response.status = false;
                    return response;
                }

                var offers = new Offers
                {
                    applicationId = offersDTO.applicationId,
                    annualPackage = offersDTO.annualPackage,
                    joiningDate = offersDTO.joiningDate,
                    timing = offersDTO.timing,
                    position = offersDTO.position,
                    description = offersDTO.description,
                    offerLetter = offerLetterPDF,
                    status = offersDTO.status,
                    reason = offersDTO.reason,
                    createdAt = DateTime.Now
                };

                await offersRepository.newOffers(offers);

                var emailBody = emailService.createOfferLetterEmailBody(
                    offersDTO.studentName,
                    offersDTO.position,
                    offersDTO.companyName,
                    offersDTO.annualPackage,
                    offersDTO.joiningDate,
                    offersDTO.timing,
                    offersDTO.description,
                    offerLetterPDF
                );

                try
                {
                    var application = await applicationsRepository.getApplicationDetailsById((int)offersDTO.applicationId);

                    var studentEmail = offers?.Applications?.Students?.Users?.email;

                    if (string.IsNullOrEmpty(studentEmail))
                    {
                        response.data = "Offer letter created but email not sent.";
                        response.message = "Email address not found.";
                        response.status = false;
                        return response;
                    }

                    await emailService.sendEmail(studentEmail, $"Job Offer from {offersDTO.companyName}", emailBody);
                }
                catch (Exception emailEx)
                {
                    Console.WriteLine("Email send error: " + emailEx.Message);

                    response.data = "Offer letter created but email failed.";
                    response.message = "Offer saved, but failed to send email.";
                    response.status = false;
                    return response;
                }

                response.data = "Offer letter created and email sent.";
                response.message = "New offer letter created successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OffersService in method newOffers: {ex.Message}");
                throw;
            }
        }

    }
}
