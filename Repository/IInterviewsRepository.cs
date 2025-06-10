using College2Career.Models;

namespace College2Career.Repository
{
    public interface IInterviewsRepository
    {
        public Task<Interviews> getApplicationById(int applicationId);
        public Task interviewSchedule(Interviews interviews);
    }
}
