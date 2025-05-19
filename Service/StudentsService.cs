using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;
using Microsoft.VisualBasic;

namespace College2Career.Service
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository studentsRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IEmailService emailService;

        public StudentsService(IStudentsRepository studentsRepository, ICloudinaryService cloudinaryService, IEmailService emailService)
        {
            this.studentsRepository = studentsRepository;
            this.cloudinaryService = cloudinaryService;
            this.emailService = emailService;
        }

        public async Task<ServiceResponse<string>> createStudentProfile(StudentsDTO studentsDTO, int usersId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var resumeURL = await cloudinaryService.uploadImages(studentsDTO.resume);

                if (resumeURL == null)
                {
                    response.data = "0";
                    response.message = "Resume upload failed.";
                    response.status = false;
                    return response;
                }

                if (usersId == null || usersId == 0)
                {
                    response.data = "0";
                    response.message = "Unauthorized! Please login again. ";
                    response.status = false;
                }

                var existingStudent = await studentsRepository.getStudentsProfileByUsersId(usersId);
                if (existingStudent != null)
                {
                    response.data = "0";
                    response.message = "Your profile is already exists!";
                    response.status = false;
                    return response;
                }

                var existingRollNumber = await studentsRepository.getStudentByRollNumber(studentsDTO.rollNumber);
                if (existingRollNumber != null)
                {
                    response.data = "0";
                    response.message = "Roll number already exists!";
                    response.status = false;
                    return response;
                }

                var newStudent = new Students
                {
                    usersId = usersId,
                    studentName = studentsDTO.studentName,
                    rollNumber = studentsDTO.rollNumber,
                    course = studentsDTO.course,
                    graduationYear = studentsDTO.graduationYear,
                    resume = resumeURL,
                    createdAt = DateTime.Now,
                };

                await studentsRepository.createStudentProfile(newStudent);
                response.data = "1";
                response.message = "Profile created successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in createStudentProfile method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<StudentsDTO>>> getStudentsProfileByUsersId(int usersId)
        {
            try
            {
                var response = new ServiceResponse<List<StudentsDTO>>();

                var existStudent = await studentsRepository.getStudentsProfileByUsersId(usersId);
                if (existStudent == null)
                {
                    response.data = new List<StudentsDTO>();
                    response.message = "No student profile found.";
                    response.status = true;
                }
                else
                {
                    var studentDTO = new StudentsDTO
                    {
                        studentId = existStudent.studentId,
                        studentName = existStudent.studentName,
                        rollNumber = existStudent.rollNumber,
                        course = existStudent.course,
                        graduationYear = existStudent.graduationYear,
                        resumeURL = existStudent.resume,
                        status = existStudent.status,
                        statusReason = existStudent.statusReason,
                        createdAt = existStudent.createdAt
                    };
                    response.data = new List<StudentsDTO> { studentDTO };
                    response.message = "Student profile found.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in getStudentsProfileByUsersId method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> updateStudentStatus(int studentId, string status, string statusReason)
        {
            try
            {
                ServiceResponse<string> response = new ServiceResponse<string>();

                var existStudent = await studentsRepository.updateStudentStatus(studentId, status, statusReason);
                if (existStudent == null)
                {
                    response.data = "0";
                    response.message = "Student not found.";
                    response.status = false;
                }
                else
                {
                    int roleIdIs = (int)(existStudent.Users.roleId);
                    Console.WriteLine(roleIdIs);
                    if (status == "activated")
                    {
                        string subject = "Profile Verification";
                        string body = emailService.createActivetedEmailBody(existStudent.studentName, (int)(existStudent.Users.roleId));
                        await emailService.sendEmail(existStudent.Users.email, subject, body);
                    }
                    else if (status == "rejected")
                    {
                        string subject = "Profile Verification";
                        string body = emailService.createRejectedEmailBody(existStudent.studentName, existStudent.statusReason, (int)(existStudent.Users.roleId));
                        await emailService.sendEmail(existStudent.Users.email, subject, body);
                    }
                    else if (status == "deactivated")
                    {
                        string subject = "Profile Verification";
                        string body = emailService.createDeactivatedEmailBody(existStudent.studentName, existStudent.statusReason, (int)(existStudent.Users.roleId));
                        await emailService.sendEmail(existStudent.Users.email, subject, body);
                    }

                    response.data = "1";
                    response.message = "Student status successfully.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in updateStudentStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<StudentsDTO>>> getAllStudents()
        {
            try
            {
                var response = new ServiceResponse<List<StudentsDTO>>();

                var existStudents = await studentsRepository.getAllStudents();

                if (existStudents == null)
                {
                    response.data = new List<StudentsDTO>();
                    response.message = "No students found.";
                    response.status = false;
                }
                else
                {
                    var allCompannies = existStudents.Select(s => new StudentsDTO
                    {
                        studentId = s.studentId,
                        studentName = s.studentName,
                        email = s.Users.email,
                        rollNumber = s.rollNumber,
                        course = s.course,
                        graduationYear = s.graduationYear,
                        resumeURL = s.resume,
                        status = s.status,
                        statusReason = s.statusReason,
                        usersId = s.usersId,
                    }).ToList();

                    response.data = allCompannies;
                    response.message = "All students found.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in getAllStudents method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<StudentsDTO>>> getStudentsByPendingStatus()
        {
            try
            {
                var response = new ServiceResponse<List<StudentsDTO>>();

                var existStudents = await studentsRepository.getStudentsByPendingStatus();

                if (existStudents == null || !existStudents.Any())
                {
                    response.data = new List<StudentsDTO>();
                    response.message = "No pending students found.";
                    response.status = false;
                }
                else
                {
                    var pendingStudents = existStudents.Select(s => new StudentsDTO
                    {
                        studentId = s.studentId,
                        studentName = s.studentName,
                        email = s.Users.email,
                        rollNumber = s.rollNumber,
                        course = s.course,
                        graduationYear = s.graduationYear,
                        resumeURL = s.resume,
                        status = s.status,
                        statusReason = s.statusReason,
                        usersId = s.usersId
                    }).ToList();
                    response.data = pendingStudents;
                    response.message = "Not status students found.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in getStudentsByPendingStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<StudentsDTO>>> getStudentsByActivatedStatus()
        {
            try
            {
                var response = new ServiceResponse<List<StudentsDTO>>();

                var activatedStudents = await studentsRepository.getStudentsByActivatedStatus();

                if (activatedStudents == null || !activatedStudents.Any())
                {
                    response.data = new List<StudentsDTO>();
                    response.message = "No activated students found.";
                    response.status = false;
                }
                else
                {
                    var activatedStudent = activatedStudents.Select(s => new StudentsDTO
                    {
                        studentId = s.studentId,
                        studentName = s.studentName,
                        email = s.Users.email,
                        rollNumber = s.rollNumber,
                        course = s.course,
                        graduationYear = s.graduationYear,
                        resumeURL = s.resume,
                        status = s.status,
                        statusReason = s.statusReason,
                        usersId = s.usersId
                    }).ToList();

                    response.data = activatedStudent;
                    response.message = "Activated students found.";
                    response.status = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in getStudentsByActivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<StudentsDTO>>> getStudentsByRejectedStatus()
        {
            try
            {
                var response = new ServiceResponse<List<StudentsDTO>>();

                var rejectedStudents = await studentsRepository.getStudentsByRejectedStatus();

                if (rejectedStudents == null || !rejectedStudents.Any())
                {
                    response.data = new List<StudentsDTO>();
                    response.message = "No rejected students found.";
                    response.status = false;
                }
                else
                {
                    var rejectedStudent = rejectedStudents.Select(s => new StudentsDTO
                    {
                        studentId = s.studentId,
                        studentName = s.studentName,
                        email = s.Users.email,
                        rollNumber = s.rollNumber,
                        course = s.course,
                        graduationYear = s.graduationYear,
                        resumeURL = s.resume,
                        status = s.status,
                        statusReason = s.statusReason,
                        usersId = s.usersId
                    }).ToList();

                    response.data = rejectedStudent;
                    response.message = "Rejected students found.";
                    response.status = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in getStudentsByRejectedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<StudentsDTO>>> getStudentsByDeactivatedStatus()
        {
            try
            {
                var response = new ServiceResponse<List<StudentsDTO>>();

                var deactivatedStudents = await studentsRepository.getStudentsByDeactivatedStatus();

                if (deactivatedStudents == null || !deactivatedStudents.Any())
                {
                    response.data = new List<StudentsDTO>();
                    response.message = "No deactivated students found.";
                    response.status = false;
                }
                else
                {
                    var deactivatedStudent = deactivatedStudents.Select(s => new StudentsDTO
                    {
                        studentId = s.studentId,
                        studentName = s.studentName,
                        email = s.Users.email,
                        rollNumber = s.rollNumber,
                        course = s.course,
                        graduationYear = s.graduationYear,
                        resumeURL = s.resume,
                        status = s.status,
                        statusReason = s.statusReason,
                        usersId = s.usersId
                    }).ToList();

                    response.data = deactivatedStudent;
                    response.message = "Deactivated students found.";
                    response.status = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in getStudentsByDeactivatedStatus method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<string>> updateStudentProfileByStudentId(StudentUpdateProfileDTO studentUpdateProfileDTO, int studentId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var existStudent = await studentsRepository.getStudentProfileByStudentId(studentId);

                if (existStudent == null || existStudent.Users == null)
                {
                    response.data = "0";
                    response.message = "Student not found or user details missing.";
                    response.status = false;
                }
                else
                {
                    Console.WriteLine("the resumeURL: " + studentUpdateProfileDTO.resumeURL);
                    var resumeURL = await cloudinaryService.uploadImages(studentUpdateProfileDTO.resume);

                    if (resumeURL == null)
                    {
                        response.data = "0";
                        response.message = "Resume upload failed.";
                        response.status = false;
                        return response;
                    }
                    existStudent.Users.email = studentUpdateProfileDTO.email;
                    existStudent.studentName = studentUpdateProfileDTO.studentName;
                    existStudent.resume = resumeURL;

                    await studentsRepository.updateStudentProfileByStudentId(existStudent, studentId);

                    response.data = "1";
                    response.message = "Profile updated successfully.";
                    response.status = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student service in updateStudentProfileByStudentId method: " + ex.Message);
                throw;
            }
        }


    }
}
