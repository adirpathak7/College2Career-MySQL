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
                var existStudent = await c2CDBContext.Students.FirstOrDefaultAsync(c => c.usersId == usersId);
                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentsProfileByUsersId method: " + ex.Message);
                throw;
            }
        }

        public async Task<Students> getStudentProfileByStudentId(int studentId)
        {
            try
            {
                var existStudent = await c2CDBContext.Students
                    .Include(s => s.Users)
                    .FirstOrDefaultAsync(s => s.studentId == studentId);
                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentProfileByStudentId method: " + ex.Message);
                throw;
            }
        }

        public async Task<Students> getStudentByRollNumber(string rollNumber)
        {
            try
            {
                var existStudent = await c2CDBContext.Students.FirstOrDefaultAsync(c => c.rollNumber == rollNumber);
                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentByRollNumber method: " + ex.Message);
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
                Console.WriteLine("ERROR occured in student repository in updateStudentStatus method: " + ex.Message);
                throw;
            }
        }
        public async Task<List<Students>> getAllStudents()
        {
            try
            {
                var allStudents = await c2CDBContext.Students.Include(u => u.Users).ToListAsync();
                return allStudents;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getAllStudents method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Students>> getStudentsByPendingStatus()
        {
            try
            {
                var existStudent = await c2CDBContext.Students.Include(u => u.Users).Where(s => s.status == "pending").ToListAsync()
                                ?? throw new Exception("Student not found for the given user ID.");
                return existStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentsByPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Students>> getStudentsByActivatedStatus()
        {
            try
            {
                var pendingStudents = await c2CDBContext.Students.Include(u => u.Users).Where(c => c.status == "activated").ToListAsync();
                return pendingStudents;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentsByActivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Students>> getStudentsByRejectedStatus()
        {
            try
            {
                var pendingStudents = await c2CDBContext.Students.Include(u => u.Users).Where(c => c.status == "rejected").ToListAsync();
                return pendingStudents;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentsByRejectedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Students>> getStudentsByDeactivatedStatus()
        {
            try
            {
                var pendingStudents = await c2CDBContext.Students.Include(u => u.Users).Where(c => c.status == "deactivated").ToListAsync();
                return pendingStudents;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in getStudentsByDeactivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task updateStudentProfileByStudentId(Students students, int studentId)
        {
            try
            {
                var existStudent = getStudentProfileByStudentId(studentId);

                if (existStudent != null)
                {
                    existStudent.Result.studentName = students.studentName;
                    existStudent.Result.resume = students.resume;
                    existStudent.Result.Users.email = students.Users.email;
                    await c2CDBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Student not found for the given user ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in student repository in updateStudentProfileByStudentId method: " + ex.Message);
                throw;
            }
        }
    }
}
