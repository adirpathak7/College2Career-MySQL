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


        [Authorize]
        [HttpPost]
        [Route("createVacancies")]
        public async Task<IActionResult> createVacancies([FromForm] VacanciesDTO vacanciesDTO)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("usersId")?.Value ?? "0");
                var result = await vacanciesService.createVacancies(vacanciesDTO, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in vacancies controller in createVacancies method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
