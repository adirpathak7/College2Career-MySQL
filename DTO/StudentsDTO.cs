namespace College2Career.DTO
{
    public class StudentsDTO
    {
        public int? studentId { get; set; }
        public string? studentName { get; set; }
        public string? email { get; set; }
        public string? rollNumber { get; set; }
        public string? course { get; set; }
        public string? graduationYear { get; set; }
        public IFormFile? resume { get; set; }
        public string? resumeURL { get; set; }
        public string? status { get; set; }
        public string? statusReason { get; set; }
        public int? usersId { get; set; }
        public DateTime? createdAt { get; set; } = DateTime.Now;
    }
}
