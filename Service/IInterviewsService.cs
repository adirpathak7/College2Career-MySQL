using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface IInterviewsService
    {
        Task<ServiceResponse<string>> interviewSchedule(InterviewsDTO interviewsDTO, int userId);
    }
}
