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

        [Authorize(Roles = "company")]
        [HttpGet]
        [Route("getAllInterviewsByCompanyId")]
        public async Task<IActionResult> getAllInterviewsByCompanyId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId").Value ?? "0");
                var result = await interviewsService.getAllInterviewsByCompanyId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsController in getAllInterviewsByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getAllInterviewsByCompanyIdToAdmin/{companyId}")]
        public async Task<IActionResult> getAllInterviewsByCompanyIdToAdmin(int companyId)
        {
            try
            {
                var result = await interviewsService.getAllInterviewsByCompanyIdToAdmin(companyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsController in getAllInterviewsByCompanyIdToAdmin method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpPut]
        [Route("rescheduledInterview/{interviewId}")]
        public async Task<IActionResult> rescheduledInterview([FromForm] AllInterviewsDTO allInterviewsDTO, int interviewId)
        {
            try
            {
                var result = await interviewsService.rescheduledInterview(allInterviewsDTO, interviewId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsController in rescheduledInterview method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpPut]
        [Route("cancelledInterview/{interviewId}")]
        public async Task<IActionResult> cancelledInterview([FromForm] AllInterviewsDTO allInterviewsDTO, int interviewId)
        {
            try
            {
                var result = await interviewsService.cancelledInterview(allInterviewsDTO, interviewId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in InterviewsController in cancelledInterview method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}