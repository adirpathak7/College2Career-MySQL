namespace College2Career.DTO
{
    public class StudentUpdateProfileDTO
    {
        public string? email { get; set; }
        public string? studentName { get; set; }
        public IFormFile? resume { get; set; }
        public string? resumeURL { get; set; }
    }
}
