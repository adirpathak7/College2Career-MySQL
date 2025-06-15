using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class InterviewsService : IInterviewsService
    {
        private readonly IInterviewsRepository interviewsRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IEmailService emailService;

        public InterviewsService(IInterviewsRepository interviewsRepository, ICompaniesRepository companiesRepository, IEmailService emailService)
        {
            this.interviewsRepository = interviewsRepository;
            this.companiesRepository = companiesRepository;
            this.emailService = emailService;
        }

        public async Task<ServiceResponse<string>> interviewSchedule(InterviewsDTO interviewsDTO, int usersId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var existCompany = await companiesRepository.getCompanyProfileByUsersId(usersId);

                if (existCompany == null)
                {
                    response.data = "0";
                    response.message = "Company not found for the user.";
                    response.status = false;
                    return response;
                }

                var existingApplicationIdForInterview = await interviewsRepository.getApplicationById((int)interviewsDTO.applicationId);
                Console.WriteLine(existingApplicationIdForInterview.applicationId);
                if (existingApplicationIdForInterview != null)
                {
                    response.data = "0";
                    response.message = "Already interview scheduled!";
                    response.status = false;
                    return response;
                }

                var newInterview = new Interviews
                {
                    applicationId = interviewsDTO.applicationId,
                    interviewDate = interviewsDTO.interviewDate,
                    interviewTime = interviewsDTO.interviewTime,
                };

                await interviewsRepository.interviewSchedule(newInterview);

                response.data = "1";
                response.message = "Interview scheduled successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsService in interviewSchedule method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<AllInterviewsDTO>>> getAllInterviewsByCompanyId(int usersId)
        {
            try
            {
                var response = new ServiceResponse<List<AllInterviewsDTO>>();

                var existCompany = await companiesRepository.getCompanyProfileByUsersId(usersId);
                if (existCompany == null)
                {
                    response.data = null;
                    response.message = "Company not found for the user.";
                    response.status = false;
                    return response;
                }

                var interviews = await interviewsRepository.getAllInterviewsByCompanyId(existCompany.companyId);
                if (interviews == null || interviews.Count == 0)
                {
                    response.data = null;
                    response.message = "No interviews found for the company.";
                    response.status = false;
                    return response;
                }

                var allInterviewsDTO = interviews.Select(i => new AllInterviewsDTO
                {
                    interviewId = i.interviewId,
                    applicationId = i.applicationId,
                    interviewDate = i.interviewDate,
                    interviewTime = i.interviewTime,
                    interviewStatus = i.interviewStatus,
                    reason = i.reason,
                    createdAt = i.createdAt,
                    updatedAt = i.updatedAt,
                    studentName = i.Applications.Students.studentName,
                    email = i.Applications.Students.Users.email,
                    vacancyTitle = i.Applications.Vacancies.title,
                    annualPackage = i.Applications.Vacancies.annualPackage,
                    companyName = existCompany.companyName
                }).ToList();

                response.data = allInterviewsDTO;
                response.message = "Interviews retrieved successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsService in getAllInterviewsByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<AllInterviewsDTO>>> getAllInterviewsByCompanyIdToAdmin(int companyId)
        {
            try
            {
                var response = new ServiceResponse<List<AllInterviewsDTO>>();

                var existCompany = await companiesRepository.getCompanyProfileByCompanyId(companyId);
                if (existCompany == null)
                {
                    response.data = null;
                    response.message = "Company not found for the user.";
                    response.status = false;
                    return response;
                }

                var interviews = await interviewsRepository.getAllInterviewsByCompanyId(companyId);
                if (interviews == null || interviews.Count == 0)
                {
                    response.data = null;
                    response.message = "No interviews found for the company.";
                    response.status = false;
                    return response;
                }

                var allInterviewsDTO = interviews.Select(i => new AllInterviewsDTO
                {
                    interviewId = i.interviewId,
                    applicationId = i.applicationId,
                    interviewDate = i.interviewDate,
                    interviewTime = i.interviewTime,
                    interviewStatus = i.interviewStatus,
                    reason = i.reason,
                    createdAt = i.createdAt,
                    updatedAt = i.updatedAt,
                    studentName = i.Applications.Students.studentName,
                    email = i.Applications.Students.Users.email,
                    vacancyTitle = i.Applications.Vacancies.title,
                    annualPackage = i.Applications.Vacancies.annualPackage,
                    companyName = existCompany.companyName
                }).ToList();

                response.data = allInterviewsDTO;
                response.message = "Interviews retrieved successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsService in getAllInterviewsByCompanyIdToAdmin method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> rescheduledInterview(AllInterviewsDTO allInterviewsDTO, int interviewId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var existingInterview = await interviewsRepository.getInterviewsByInterviewId(interviewId);
                if (existingInterview == null)
                {
                    response.data = "0";
                    response.message = "Interview not found!";
                    response.status = false;
                    return response;
                }

                existingInterview.interviewDate = allInterviewsDTO.interviewDate;
                existingInterview.interviewTime = allInterviewsDTO.interviewTime;
                existingInterview.interviewStatus = "rescheduled";
                existingInterview.reason = allInterviewsDTO.reason;
                existingInterview.updatedAt = DateTime.Now;

                await interviewsRepository.rescheduledInterview(existingInterview);

                var interviewDateTime = $"{allInterviewsDTO.interviewDate} at {allInterviewsDTO.interviewTime}";

                var emailBody = emailService.createInterviewStatusEmailBody(
                    allInterviewsDTO.studentName,
                    "rescheduled",
                    allInterviewsDTO.companyName,
                    allInterviewsDTO.vacancyTitle,
                    interviewDateTime,
                    allInterviewsDTO.reason
                );
                //Console.WriteLine("allInterviewsDTO student email:- " + allInterviewsDTO?.email);

                //Console.WriteLine("with + student email:- " + existingInterview.Applications?.Students?.Users?.email);

                await emailService.sendEmail(existingInterview.Applications?.Students?.Users?.email, "Interview Rescheduled - College2Career", emailBody);

                response.data = "1";
                response.message = "Interview rescheduled successfully.";
                response.status = true;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsService in rescheduledInterview method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> cancelledInterview(AllInterviewsDTO allInterviewsDTO, int interviewId)
        {
            try
            {
                var response = new ServiceResponse<string>();
                var existingInterview = await interviewsRepository.getInterviewsByInterviewId(interviewId);
                if (existingInterview == null)
                {
                    response.data = "0";
                    response.message = "Interview not found!";
                    response.status = false;
                    return response;
                }

                existingInterview.interviewStatus = "cancelled";
                existingInterview.reason = allInterviewsDTO.reason;
                existingInterview.updatedAt = DateTime.Now;

                await interviewsRepository.cancelledInterview(existingInterview);

                var emailBody = emailService.createInterviewStatusEmailBody(
                    allInterviewsDTO.studentName,
                    "cancelled",
                    allInterviewsDTO.companyName,
                    allInterviewsDTO.vacancyTitle,
                    allInterviewsDTO.interviewDate + " at " + allInterviewsDTO.interviewTime,
                    allInterviewsDTO.reason
                );

                await emailService.sendEmail(existingInterview.Applications?.Students?.Users?.email, "Interview Cancelled - College2Career", emailBody);

                response.data = "1";
                response.message = "Interview cancelled successfully.";
                response.status = true;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsService in cancelledInterview method: " + ex.Message);
                throw;
            }
        }
    }
}