using College2Career.DTO;
using College2Career.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/college/")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly ICollegesService collegesService;

        public CollegesController(ICollegesService collegesService)
        {
            this.collegesService = collegesService;
        }


        [HttpPost]
        [Route("addCollegesInformation")]
        public async Task<IActionResult> addCollegesInformation(CollegesDTO collegesDTO)
        {
            try
            {
                var result = await collegesService.addCollegesInformation(collegesDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in company controller in addCollegesInformation method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
