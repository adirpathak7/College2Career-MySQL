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
        public InterviewsService(IInterviewsRepository interviewsRepository, ICompaniesRepository companiesRepository)
        {
            this.interviewsRepository = interviewsRepository;
            this.companiesRepository = companiesRepository;
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
    }
}
