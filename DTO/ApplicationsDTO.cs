using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.DTO
{
    public class ApplicationsDTO
    {
        public int? studentId { get; set; }
        public int? vacancyId { get; set; }
        public string? reason { get; set; }
        public string? status { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
