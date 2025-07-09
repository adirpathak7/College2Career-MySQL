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
        [Route("getCompanyProfileByUsersId")]
        public async Task<IActionResult> getCompanyProfileByUsersId()
        {
            try
            {
                var extractedUserId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await companiesService.getCompanyProfileByUsersId(extractedUserId);
                if (result == null)
                {
                    return NotFound(new { message = "Company profile not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompanyProfileByUsersId method: " + ex.Message);
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

        [HttpGet]
        [Route("getAllCompanies")]
        public async Task<IActionResult> getAllCompanies()
        {
            try
            {
                var result = await companiesService.getAllCompanies();
                if (result == null)
                {
                    return NotFound(new { message = "No company found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getAllCompanies method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCompaniesByPendingStatus")]
        public async Task<IActionResult> getCompaniesPendingStatus()
        {
            try
            {
                var result = await companiesService.getCompaniesByPendingStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No pending company found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompaniesPendingStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCompaniesByActivatedStatus")]
        public async Task<IActionResult> getCompaniesActivatedStatus()
        {
            try
            {
                var result = await companiesService.getCompaniesByActivatedStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No activated company found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompaniesByActivatedStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCompaniesByRejectedStatus")]
        public async Task<IActionResult> getCompaniesRejectedStatus()
        {
            try
            {
                var result = await companiesService.getCompaniesByRejectedStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No rejected company found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompaniesByRejectedStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCompaniesByDeactivatedStatus")]
        public async Task<IActionResult> getCompaniesDeactivatedStatus()
        {
            try
            {
                var result = await companiesService.getCompaniesByDeactivatedStatus();
                if (result == null)
                {
                    return NotFound(new { message = "No deactivated company found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompaniesByDeactivatedStatus method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("getCompanyDashboardStats")]
        public async Task<IActionResult> getCompanyDashboardStats()
        {
            try
            {
                var extractedUserId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await companiesService.getCompanyDashboardStats(extractedUserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in getCompanyDashboardStats method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

    }
}
