using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;

namespace College2Career.Service
{
    public interface IInterviewsService
    {
        Task<ServiceResponse<string>> interviewSchedule(InterviewsDTO interviewsDTO, int usersId);
        Task<ServiceResponse<List<AllInterviewsDTO>>> getAllInterviewsByCompanyId(int usersId);
        Task<ServiceResponse<List<AllInterviewsDTO>>> getAllInterviewsByCompanyIdToAdmin(int companyId);
        Task<ServiceResponse<string>> rescheduledInterview(AllInterviewsDTO allInterviewsDTO, int interviewId);
        Task<ServiceResponse<string>> cancelledInterview(AllInterviewsDTO allInterviewsDTO, int interviewId);
    }
}
