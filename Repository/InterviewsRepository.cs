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

        public async Task<List<Interviews>> getAllInterviewsByCompanyId(int usersId)
        {
            try
            {
                var interviews = await c2CDBContext.Interviews
                    .Include(i => i.Applications)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(v => v.Applications.Vacancies)
                    .Where(i => i.Applications.Vacancies.Companies.companyId == usersId)
                    .ToListAsync();
                return interviews;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in getAllInterviewsByCompanyId method: " + ex.Message);
                throw;
            }
        }
        public async Task<List<Interviews>> getAllInterviewsByCompanyIdToAdmin(int companyId)
        {
            try
            {
                var interviews = await c2CDBContext.Interviews
                    .Include(i => i.Applications)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(v => v.Applications.Vacancies)
                    .Where(i => i.Applications.Vacancies.Companies.companyId == companyId)
                    //.Where(i => i.Applications != null && i.Applications.Vacancies.Companies != null && i.Applications.Students != null && i.Applications.Vacancies.Companies.companyId == companyId)
                    .ToListAsync();
                return interviews;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in getAllInterviewsByCompanyId method: " + ex.Message);
                throw;
            }
        }
        public async Task<Interviews> getInterviewsByInterviewId(int interviewId)
        {
            try
            {
                var interview = await c2CDBContext.Interviews.FirstOrDefaultAsync(i => i.interviewId == interviewId);
                return interview;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in getInterviewsByInterviewId method: " + ex.Message);
                throw;
            }
        }
        public async Task<Interviews> rescheduledInterview(Interviews interviews)
        {
            try
            {
                var existingInterview = await c2CDBContext.Interviews
                    .Include(a => a.Applications.Vacancies)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(c => c.Applications.Vacancies.Companies)
                    .FirstOrDefaultAsync(i => i.interviewId == interviews.interviewId);

                if (existingInterview != null)
                {
                    existingInterview.interviewDate = interviews.interviewDate;
                    existingInterview.interviewTime = interviews.interviewTime;
                    existingInterview.interviewStatus = interviews.interviewStatus;
                    existingInterview.reason = interviews.reason;
                    existingInterview.updatedAt = DateTime.Now;

                    c2CDBContext.Interviews.Update(existingInterview);
                    await c2CDBContext.SaveChangesAsync();
                }
                return existingInterview;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in rescheduledInterview method: " + ex.Message);
                throw;
            }
        }

        public async Task<Interviews> cancelledInterview(Interviews interviews)
        {
            try
            {
                var existingInterview = await c2CDBContext.Interviews
                    .Include(a => a.Applications.Vacancies)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(c => c.Applications.Vacancies.Companies)
                    .FirstOrDefaultAsync(i => i.interviewId == interviews.interviewId);

                if (existingInterview != null)
                {
                    existingInterview.interviewStatus = interviews.interviewStatus;
                    existingInterview.reason = interviews.reason;
                    existingInterview.updatedAt = DateTime.Now;

                    c2CDBContext.Interviews.Update(existingInterview);
                    await c2CDBContext.SaveChangesAsync();
                }
                return existingInterview;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in cancelledInterview method: " + ex.Message);
                throw;
            }
        }

        public async Task<Interviews> completedInterview(Interviews interviews)
        {
            try
            {
                var existingInterview = await c2CDBContext.Interviews
                    .Include(a => a.Applications.Vacancies)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(c => c.Applications.Vacancies.Companies)
                    .FirstOrDefaultAsync(i => i.interviewId == interviews.interviewId);

                if (existingInterview != null)
                {
                    existingInterview.interviewStatus = interviews.interviewStatus;
                    existingInterview.reason = interviews.reason;
                    existingInterview.updatedAt = DateTime.Now;

                    c2CDBContext.Interviews.Update(existingInterview);
                    await c2CDBContext.SaveChangesAsync();
                }
                return existingInterview;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in completedInterview method: " + ex.Message);
                throw;
            }
        }

        public async Task<Interviews> offeredInterview(Interviews interviews)
        {
            try
            {
                var existingInterview = await c2CDBContext.Interviews
                    .Include(a => a.Applications.Vacancies)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(c => c.Applications.Vacancies.Companies)
                    .FirstOrDefaultAsync(i => i.interviewId == interviews.interviewId);

                if (existingInterview != null)
                {
                    existingInterview.interviewStatus = interviews.interviewStatus;
                    existingInterview.reason = interviews.reason;
                    existingInterview.updatedAt = DateTime.Now;

                    c2CDBContext.Interviews.Update(existingInterview);
                    await c2CDBContext.SaveChangesAsync();
                }
                return existingInterview;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in offeredInterview method: " + ex.Message);
                throw;
            }
        }
        public Task<List<Interviews>> getAllScheduledInterviewsByCompanyId(int companyId)
        {
            try
            {
                var interviews = c2CDBContext.Interviews
                    .Include(i => i.Applications)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(v => v.Applications.Vacancies)
                    .Where(i => i.Applications.Vacancies.Companies.companyId == companyId && i.interviewStatus == "scheduled")
                    .ToListAsync();
                return interviews;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in getAllScheduledInterviewsByCompanyId method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Interviews>> getAllCompletedInterviewsByCompanyId(int companyId)
        {
            try
            {
                var completedVacancies = await c2CDBContext.Interviews
                    .Include(i => i.Applications)
                    .Include(s => s.Applications.Students)
                    .Include(su => su.Applications.Students.Users)
                    .Include(v => v.Applications.Vacancies)
                    .Where(i => i.Applications.Vacancies.Companies.companyId == companyId && i.interviewStatus == "completed")
                    .ToListAsync();
                return completedVacancies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsRepository in getAllCompletedInterviewsByCompanyId method: " + ex.Message);
                throw;
            }
        }
    }
}