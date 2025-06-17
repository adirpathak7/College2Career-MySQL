using College2Career.Models;

namespace College2Career.Repository
{
    public interface IInterviewsRepository
    {
        Task<Interviews> getApplicationById(int applicationId);
        Task interviewSchedule(Interviews interviews);
        Task<List<Interviews>> getAllInterviewsByCompanyIdToAdmin(int companyId);
        Task<List<Interviews>> getAllInterviewsByCompanyId(int usersId);
        Task<Interviews> getInterviewsByInterviewId(int interviewId);
        Task<Interviews> rescheduledInterview(Interviews interviews);
        Task<Interviews> cancelledInterview(Interviews interviews);
        Task<Interviews> completedInterview(Interviews interviews);
        Task<Interviews> offeredInterview(Interviews interviews);
        Task<List<Interviews>> getAllScheduledInterviewsByCompanyId(int companyId);
        Task<List<Interviews>> getAllCompletedInterviewsByCompanyId(int companyId);
    }
}
