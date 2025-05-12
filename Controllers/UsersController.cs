using Azure;
using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace College2Career.Controllers
{
    [Route("api/college2career/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("hello")]
        public string hello()
        {
            return "Hello World";
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> signUpUsers([FromForm] UsersDTO usersDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var newUser = await usersService.signUpUsers(usersDTO);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = ex.Message;
                response.status = false;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> signInUsers([FromForm] UsersAuthDTO usersAuthDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var existUser = await usersService.signInUsers(usersAuthDTO);
                return Ok(existUser);
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = ex.Message;
                response.status = false;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> forgotPassword([FromForm] ForgotValidEmail forgotValidEmail)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var result = await usersService.findUserByEmail(forgotValidEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = ex.Message;
                response.status = false;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> resetPassword([FromForm] ForgotPasswordDTO forgotPasswordDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {

                var result = await usersService.forgotPassword(forgotPasswordDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = ex.Message;
                response.status = false;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> getAllUsers([FromQuery] AllUsersDTO allUsersDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var result = await usersService.getAllUsers(allUsersDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = ex.Message;
                response.status = false;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}