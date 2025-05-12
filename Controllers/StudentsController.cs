using College2Career.DTO;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/students/")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        [Authorize]
        [HttpGet]
        [Route("helloFromStudents")]
        public string helloStudents()
        {
            return "Hello from students!";
        }

        [Authorize]
        [HttpPost]
        [Route("createStudentProfile")]
        public async Task<IActionResult> createStudentProfile([FromForm] StudentsDTO studentsDTO)
        {
            try
            {
                var extractedUserId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await studentsService.createStudentProfile(studentsDTO, extractedUserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student controller in createStudentProfile method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("getStudentProfileByUsersId")]
        public async Task<IActionResult> getStudentProfileByUsersId()
        {
            try
            {
                var extractedUserId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await studentsService.getStudentsProfileByUsersId(extractedUserId);
                if (result == null)
                {
                    return NotFound(new { message = "Student profile not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student controller in getStudentProfileByUsersId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getStudentByPendingStatus")]
        public async Task<IActionResult> getStudentByPendingStatus()
        {
            try
            {
                var result = await studentsService.getStudentByPendingStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No pending students found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student controller in getStudentByPendingStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpPatch]
        [Route("updateStudentStatus/{studentId}")]
        public async Task<IActionResult> updateStudentStatus(int studentId, [FromForm] StudentStatusDTO studentStatusDTO)
        {
            try
            {
                var result = await studentsService.updateStudentStatus(studentId, studentStatusDTO.status, studentStatusDTO.reasonOfStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student controller in updateStudentStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
