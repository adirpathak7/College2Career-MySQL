using College2Career.Data;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;

namespace College2Career.Repository
{
    public class InterviewsRepository : IInterviewsRepository
    {
        private readonly C2CDBContext c2CDBContext;

        public InterviewsRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }

        public async Task interviewSchedule(Interviews interviews)
        {
            try
            {
                await c2CDBContext.Interviews.AddAsync(interviews);
                interviews.updatedAt = DateTime.Now;
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in interviewSchedule method: " + ex.Message);
                throw;
            }
        }
        public async Task<Interviews> getApplicationById(int applicationId)
        {
            try
            {
                var existingApplicationIdForInterview = await c2CDBContext.Interviews.FirstOrDefaultAsync(a => a.applicationId == applicationId);
                return existingApplicationIdForInterview;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in getInterviewsById method: " + ex.Message);
                throw;
            }
        }
    }
}
