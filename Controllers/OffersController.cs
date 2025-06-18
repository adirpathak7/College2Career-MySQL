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

        [Authorize(Roles = "student")]
        [HttpPut]
        [Route("updateOfferStatusAccepted/{offerId}")]
        public async Task<IActionResult> updateOfferStatusAccepted(int offerId, [FromForm] OffersDTO offersDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst(u => u.Type == "usersId")?.Value);
                var result = await offersService.updateOfferStatusAccepted(offerId, offersDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in OffersController in updateOfferStatusAccepted method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [Authorize(Roles = "student")]
        [HttpPut]
        [Route("updateOfferStatusRejected/{offerId}")]
        public async Task<IActionResult> updateOfferStatusRejected(int offerId, OffersDTO offersDTO)
        {
            try
            {
                var usersId = int.Parse(User.FindFirst(u => u.Type == "usersId")?.Value);
                var result = await offersService.updateOfferStatusRejected(offerId, offersDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in OffersController in updateOfferStatusRejected method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [Authorize(Roles = "student")]
        [HttpGet]
        [Route("getAllOffersByStudentId")]
        public async Task<IActionResult> getAllOffersByStudentId()
        {
            try
            {
                var usersId = int.Parse(User.FindFirst(u => u.Type == "usersId")?.Value);
                var result = await offersService.getAllOffersByStudentId(usersId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in OffersController in getAllOffersByStudentId method: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
