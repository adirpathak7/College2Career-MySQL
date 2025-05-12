using College2Career.Models;

namespace College2Career.Repository
{
    public interface IStudentsRepository
    {
        public Task createStudentProfile(Students students);
        public Task<Students> getStudentsProfileByUsersId(int usersId);
        public Task<List<Students>> getStudentByPendingStatus();
        public Task<Students> updateStudentStatus(int studentId, string status, string statusReason);
    }
}
