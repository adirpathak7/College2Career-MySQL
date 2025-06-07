namespace College2Career.DTO
{
    public class VacanciesAppliedStudentsDTO
    {
        public int applicationId { get; set; }
        public DateTime applicationsAppliedAt { get; set; }
        public string applicationStatus { get; set; }
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string studentEmail { get; set; }
        public string studentRollNumber { get; set; }
        public string course { get; set; }
        public string graduationYear { get; set; }
        public string resumeURL { get; set; }
        public int vacancyId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string eligibility_criteria { get; set; }
        public string totalVacancy { get; set; }
        public string locationType { get; set; }
        public string vacancyStatus { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
