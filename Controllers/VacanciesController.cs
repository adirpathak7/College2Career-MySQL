using College2Career.DTO;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/companies/")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacanciesService vacanciesService;

        public VacanciesController(IVacanciesService vacanciesService)
        {
            this.vacanciesService = vacanciesService;
        }


        [Authorize(Roles = "company")]
        [HttpPost]
        [Route("createVacancies")]
        public async Task<IActionResult> createVacancies([FromForm] VacanciesDTO vacanciesDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.createVacancies(vacanciesDTO, usersId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in createVacancies method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpGet]
        [Route("getVacanciesByCompanyId")]
        public async Task<IActionResult> getVacanciesByCompanyId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.getVacanciesByCompanyId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getVacanciesByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpPut]
        [Route("updateCompanyVacanciesByVacancyId/{vacancyId}")]
        public async Task<IActionResult> updateCompanyVacanciesByVacancyId(int vacancyId, [FromBody] VacanciesDTO vacanciesDTO)
        {
            try
            {
                var companyId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.updateCompanyVacanciesByVacancyId(vacancyId, companyId, vacanciesDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in updateCompanyVacanciesByVacancyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getAllVacancies")]
        public async Task<IActionResult> getAllVacancies()
        {
            try
            {
                var result = await vacanciesService.getAllVacancies();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getAllVacancies method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }


        [Authorize(Roles = "company")]
        [HttpGet]
        [Route("getHiringVacanciesByCompanyId")]
        public async Task<IActionResult> getHiringVacanciesByCompanyId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.getHiringVacanciesByCompanyId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getHiringVacanciesByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpGet]
        [Route("getHiredVacanciesByCompanyId")]
        public async Task<IActionResult> getHiredVacanciesByCompanyId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.getHiredVacanciesByCompanyId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getHiredVacanciesByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getHiringVacancies")]
        public async Task<IActionResult> getHiringVacancies()
        {
            try
            {
                var result = await vacanciesService.getHiringVacancies();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getHiringVacancies method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("getHiredVacancies")]
        public async Task<IActionResult> getHiredVacancies()
        {
            try
            {
                var result = await vacanciesService.getHiredVacancies();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getHiredVacancies method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [Authorize(Roles = "company")]
        [HttpPatch]
        [Route("updateVacanciesStatus/{vacancyId}")]
        public async Task<IActionResult> updateVacanciesStatus(int vacancyId, [FromBody] VacanciesStatusDTO vacanciesStatusDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.updateVacanciesStatus(vacancyId, usersId, vacanciesStatusDTO.status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in getHiredVacanciesByCompanyId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
