using System.Security.Claims;
using College2Career.DTO;
using College2Career.Models;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/companies/")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly ICompaniesService companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        [Authorize]
        [HttpGet]
        [Route("helloFromCompanies")]
        public string helloCompany()
        {
            return "Hello from company!";
        }

        [Authorize]
        [HttpPost]
        [Route("createCompanyProfile")]
        public async Task<IActionResult> createCompanyProfile([FromForm] CompaniesDTO companiesDTO)
        {
            try
            {
                var extractedUsersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await companiesService.createCompanyProfile(companiesDTO, extractedUsersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in createCompanyProfile method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("getCompaniesProfileByUsersId")]
        public async Task<IActionResult> getCompaniesProfileByUsersId()
        {
            try
            {
                var extractedUserId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                Console.WriteLine("extractedUserId is: " + extractedUserId);
                var result = await companiesService.getCompaniesProfileByUsersId(extractedUserId);
                if (result == null)
                {
                    return NotFound(new { message = "Company profile not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompaniesProfileByUsersId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }


        [HttpGet]
        [Route("getCompanyByPendingStatus")]
        public async Task<IActionResult> getCompanyByPendingStatus()
        {
            try
            {
                var result = await companiesService.getCompanyByPendingStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No pending company found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompanyByPendingStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpPatch]
        [Route("updateCompanyStatus/{companyId}")]
        public async Task<IActionResult> updateCompanyStatus(int companyId, [FromBody] CompaniesStatusDTO companiesStatusDTO)
        {
            try
            {
                var result = await companiesService.updateCompanyStatus(companyId, companiesStatusDTO.status, companiesStatusDTO.reasonOfStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in updateCompanyStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

    }
}
