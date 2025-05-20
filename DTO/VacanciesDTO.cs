using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.DTO
{
    public class VacanciesDTO
    {
        public int? vacancyId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string eligibility_criteria { get; set; }
        public string totalVacancy { get; set; }
        public string timing { get; set; }
        public string package { get; set; }
        public string type { get; set; }
        public string locationType { get; set; }
        public string? status { get; set; }
        public int? companyId { get; set; }
    }
}
