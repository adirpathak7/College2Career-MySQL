using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface IStudentsService
    {
        Task<ServiceResponse<string>> createStudentProfile(StudentsDTO studentsDTO, int usersId);
        Task<ServiceResponse<List<StudentsDTO>>> getStudentsProfileByUsersId(int usersId);
        Task<ServiceResponse<string>> updateStudentStatus(int studentId, string status, string statusReason);
        Task<ServiceResponse<List<StudentsDTO>>> getAllStudents();
        Task<ServiceResponse<List<StudentsDTO>>> getStudentsByPendingStatus();
        Task<ServiceResponse<List<StudentsDTO>>> getStudentsByActivatedStatus();
        Task<ServiceResponse<List<StudentsDTO>>> getStudentsByRejectedStatus();
        Task<ServiceResponse<List<StudentsDTO>>> getStudentsByDeactivatedStatus();
        Task<ServiceResponse<string>> updateStudentProfileByStudentId(StudentUpdateProfileDTO studentUpdateProfileDTO, int usersId);
    }
}
