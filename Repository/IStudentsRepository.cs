using College2Career.Models;

namespace College2Career.Repository
{
    public interface IStudentsRepository
    {
        public Task createStudentProfile(Students students);
        public Task<Students> getStudentsProfileByUsersId(int usersId);
        public Task<Students> getStudentProfileByStudentId(int studentId);
        public Task<Students> updateStudentStatus(int studentId, string status, string statusReason);
        public Task<Students> getStudentByRollNumber(string rollNumber);
        public Task<List<Students>> getAllStudents();
        public Task<List<Students>> getStudentsByPendingStatus();
        public Task<List<Students>> getStudentsByActivatedStatus();
        public Task<List<Students>> getStudentsByRejectedStatus();
        public Task<List<Students>> getStudentsByDeactivatedStatus();
        public Task updateStudentProfileByStudentId(Students students, int studentId);
    }
}
