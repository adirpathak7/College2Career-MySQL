using College2Career.DTO;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/applications/")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationsService applicationsService;

        public ApplicationsController(IApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        [Authorize(Roles = "student")]
        [HttpPost]
        [Route("newApplications")]
        public async Task<IActionResult> newApplications([FromBody] ApplicationsDTO applicationsDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId").Value ?? "0");
                var result = await applicationsService.newApplications(applicationsDTO, usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in newApplications method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpGet]
        [Route("getAllAppliedApplicationsByCompanyId")]
        public async Task<IActionResult> getAllAppliedApplicationsByCompanyId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId").Value ?? "0");
                //Console.WriteLine("usersId in controller:- " + usersId);
                var result = await applicationsService.getAllAppliedApplicationsByCompanyId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getAllAppliedApplicationsByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}