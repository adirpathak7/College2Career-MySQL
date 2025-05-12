using College2Career.Data;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;

namespace College2Career.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly C2CDBContext c2CDBContext;

        public StudentsRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }
        public async Task createStudentProfile(Students students)
        {
            try
            {
                await c2CDBContext.Students.AddAsync(students);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in createStudentProfile method: " + ex.Message);
                throw;
            }
        }

        public async Task<Students> getStudentsProfileByUsersId(int usersId)
        {
            try
            {
                var existStudent = await c2CDBContext.Students.FirstOrDefaultAsync(c => c.usersId == usersId)
                            ?? throw new Exception("Student not found for the given user ID.");
                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in srudent repository in getStudentsProfileByUsersId method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Students>> getStudentByPendingStatus()
        {
            try
            {
                var existStudent = await c2CDBContext.Students.Where(s => s.status == "pending").ToListAsync()
                                ?? throw new Exception("Student not found for the given user ID.");
                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentByPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<Students> updateStudentStatus(int studentId, string status, string statusReason)
        {
            try
            {
                var existStudent = await c2CDBContext.Students
                .Include(c => c.Users).
                    FirstOrDefaultAsync(c => c.studentId == studentId);

                if (existStudent == null) return null;

                existStudent.status = status;
                existStudent.statusReason = statusReason;
                await c2CDBContext.SaveChangesAsync();

                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in srudent repository in updateStudentStatus method: " + ex.Message);
                throw;
            }
        }
    }
}
