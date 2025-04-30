namespace College2Career.DTO
{
    public class FeedbacksDTO
    {
        public int feedbackId { get; set; }
        public string comments { get; set; }
        public string rating { get; set; }
        public int studentId { get; set; }
        public int companyId { get; set; }
        public int collegeId { get; set; }
        public DateTime createdAt { get; set; }

    }
}
