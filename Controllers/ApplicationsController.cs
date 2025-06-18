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
                var result = await applicationsService.getAllAppliedApplicationsByCompanyId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getAllAppliedApplicationsByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpPut]
        [Route("updateApplicationsStatusByCompany/{applicationId}")]
        public async Task<IActionResult> updateApplicationsStatusByCompany(int applicationId, [FromBody] UpdateApplicationStatusDTO updateApplicationStatusDTO)
        {
            try
            {
                var result = await applicationsService.updateApplicationsStatusByCompany(applicationId, updateApplicationStatusDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in updateApplicationsStatusByCompany method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "student")]
        [HttpGet]
        [Route("getAllAppliedApplicationsByStudentId")]
        public async Task<IActionResult> getAllAppliedApplicationsByStudentId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId").Value ?? "0");
                var result = await applicationsService.getAllAppliedApplicationsByStudentId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getAllAppliedApplicationsByStudentId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "student")]
        [HttpPut]
        [Route("updateStatusToOfferedByStudentId/{applicationId}")]
        public async Task<IActionResult> updateStatusToOfferedByStudentId(int applicationId)
        {
            try
            {
                var result = await applicationsService.updateStatusToOfferedByStudentId(applicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in updateStatusToOfferedByStudentId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "student")]
        [HttpPut]
        [Route("updateStatusToOfferAcceptedStudentId/{applicationId}")]
        public async Task<IActionResult> updateStatusToOfferAcceptedStudentId(int applicationId)
        {
            try
            {
                var result = await applicationsService.updateStatusToOfferAcceptedStudentId(applicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in updateStatusToOfferAcceptedStudentId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "student")]
        [HttpPut]
        [Route("updateStatusToOfferRejectedStudentId/{applicationId}")]
        public async Task<IActionResult> updateStatusToOfferRejectedStudentId(int applicationId, [FromForm] ApplicationsDTO applicationsDTO)
        {
            try
            {
                var result = await applicationsService.updateStatusToOfferRejectedStudentId(applicationId, applicationsDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in updateStatusToOfferRejectedStudentId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}