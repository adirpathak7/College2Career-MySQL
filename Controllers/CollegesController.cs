using College2Career.Data;
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

        private readonly C2CDBContext _context;

        public CollegesController(ICollegesService collegesService, C2CDBContext context)
        {
            this.collegesService = collegesService;
            this._context = context;
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

        [HttpGet]
        [Route("getCollegeDashboardCounts")]
        public async Task<IActionResult> getDashboardCounts()
        {
            try
            {
                var totalStudents = _context.Students.Count();
                var activeStudents = _context.Students.Count(s => s.status == "active");
                var pendingStudents = _context.Students.Count(s => s.status == "pending");

                var totalCompanies = _context.Companies.Count();
                var activatedCompanies = _context.Companies.Count(c => c.status == "active");
                var pendingCompanies = _context.Companies.Count(c => c.status == "pending");

                var totalVacancies = _context.Vacancies.Count();
                var totalApplications = _context.Applications.Count();

                var offerAcceptedStudents = _context.Applications.Count(a => a.status == "offerAccepted");

                return Ok(new
                {
                    students = new
                    {
                        total = totalStudents,
                        active = activeStudents,
                        pending = pendingStudents
                    },
                    companies = new
                    {
                        total = totalCompanies,
                        active = activatedCompanies,
                        pending = pendingCompanies
                    },
                    vacancies = new
                    {
                        total = totalVacancies,
                        applications = totalApplications
                    },
                    applications = new
                    {
                        offerAccepted = offerAcceptedStudents
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in getDashboardCounts: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

    }
}
