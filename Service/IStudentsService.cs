using College2Career.DTO;
using College2Career.HelperServices;

namespace College2Career.Service
{
    public interface IStudentsService
    {
        Task<ServiceResponse<string>> createStudentProfile(StudentsDTO studentsDTO, int usersId);
        Task<ServiceResponse<List<StudentsDTO>>> getStudentsProfileByUsersId(int usersId);
        Task<ServiceResponse<List<StudentsDTO>>> getStudentByPendingStatus();
        Task<ServiceResponse<string>> updateStudentStatus(int studentId, string status, string statusReason);
    }
}
