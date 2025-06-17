using College2Career.DTO;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/users/offers/")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOffersService offersService;

        public OffersController(IOffersService offersService)
        {
            this.offersService = offersService;
        }

        [Authorize(Roles = "company")]
        [HttpPost]
        [Route("newOffers")]
        public async Task<IActionResult> newOffers([FromForm] OffersDTO offersDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst(u => u.Type == "usersId")?.Value);
                var result = await offersService.newOffers(offersDTO, usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in OffersController in newOffers method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
