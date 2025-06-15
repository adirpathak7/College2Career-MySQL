using College2Career.Models;

namespace College2Career.Repository
{
    public interface IInterviewsRepository
    {
        Task<Interviews> getApplicationById(int applicationId);
        Task interviewSchedule(Interviews interviews);
        Task<List<Interviews>> getAllInterviewsByCompanyId(int companyId);
        Task<Interviews> getInterviewsByInterviewId(int interviewId);
        Task<Interviews> rescheduledInterview(Interviews interviews);
        Task<Interviews> cancelledInterview(Interviews interviews);
    }
}
