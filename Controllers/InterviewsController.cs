using College2Career.DTO;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/interviews/")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewsService interviewsService;

        public InterviewsController(IInterviewsService interviewsService)
        {
            this.interviewsService = interviewsService;
        }

        [Authorize(Roles = "company")]
        [HttpPost]
        [Route("interviewSchedule")]
        public async Task<IActionResult> interviewSchedule([FromBody] InterviewsDTO interviewsDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId").Value ?? "0");
                var result = await interviewsService.interviewSchedule(interviewsDTO, usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsController in interviewSchedule method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
        ///users/interviews/interviewSchedule
    }
}