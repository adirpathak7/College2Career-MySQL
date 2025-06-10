namespace College2Career.DTO
{
    public class InterviewsDTO
    {
        public string interviewDate { get; set; }
        public string interviewTime { get; set; }
        public string? interviewStatus { get; set; }
        public string? reason { get; set; }
        public int? applicationId { get; set; }
        public DateTime updatedAt { get; set; }

    }
}
