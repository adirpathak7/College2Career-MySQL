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

        [Authorize(Roles = "student")]
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

        [Authorize(Roles = "student")]
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

        [HttpPatch]
        [Route("updateStudentStatus/{studentId}")]
        public async Task<IActionResult> updateStudentStatus(int studentId, [FromBody] StudentStatusDTO studentStatusDTO)
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

        [HttpGet]
        [Route("getAllStudents")]
        public async Task<IActionResult> getAllStudents()
        {
            try
            {
                var result = await studentsService.getAllStudents();
                if (result == null)
                {
                    return NotFound(new { message = "No student found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in student controller in getAllStudents method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getStudentsByPendingStatus")]
        public async Task<IActionResult> getStudentByPendingStatus()
        {
            try
            {
                var result = await studentsService.getStudentsByPendingStatus();
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

        [HttpGet]
        [Route("getStudentsByActivatedStatus")]
        public async Task<IActionResult> getStudentsByActivatedStatus()
        {
            try
            {
                var result = await studentsService.getStudentsByActivatedStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No activated students found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in students controller in getStudentsByActivatedStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getStudentsByRejectedStatus")]
        public async Task<IActionResult> getStudentsByRejectedStatus()
        {
            try
            {
                var result = await studentsService.getStudentsByRejectedStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No rejected students found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in students controller in getStudentsByRejectedStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getStudentsByDeactivatedStatus")]
        public async Task<IActionResult> getStudentsByDeactivatedStatus()
        {
            try
            {
                var result = await studentsService.getStudentsByDeactivatedStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No deactivated students found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in students controller in getStudentsByDeactivatedStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "student")]
        [HttpPut]
        [Route("updateStudentProfileByStudentId")]
        public async Task<IActionResult> updateStudentProfileByStudentId([FromForm] StudentUpdateProfileDTO studentUpdateProfileDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await studentsService.updateStudentProfileByStudentId(studentUpdateProfileDTO, usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in students controller in updateStudentProfileByStudentId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
