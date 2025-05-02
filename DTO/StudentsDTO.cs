namespace College2Career.DTO
{
    public class StudentsDTO
    {
        public int? studentId { get; set; }
        public string? studentName { get; set; }
        public string? rollNumber { get; set; }
        public string? course { get; set; }
        public string? graduationYear { get; set; }
        public string? resume { get; set; }
        public string? verified { get; set; }
        public int? userId { get; set; }
        public DateTime? createdAt { get; set; } = DateTime.Now;
    }
}
