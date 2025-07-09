using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Vacancies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vacancyId { get; set; }

        [Column("title", TypeName = "varchar(50)")]
        public string? title { get; set; }

        [Column("description", TypeName = "varchar(3000)")]
        public string? description { get; set; }

        [Column("eligibility_criteria", TypeName = "varchar(3000)")]
        public string? eligibility_criteria { get; set; }

        [Column("totalVacancy", TypeName = "varchar(10)")]
        public string? totalVacancy { get; set; }

        [Column("timing", TypeName = "varchar(50)")]
        public string? timing { get; set; } = null;

        [Column("annualPackage", TypeName = "varchar(50)")]
        public string? annualPackage { get; set; }

        [Column("type", TypeName = "varchar(15)")]
        public string? type { get; set; }

        [Column("locationType", TypeName = "varchar(15)")]
        public string? locationType { get; set; }

        [Column("status", TypeName = "varchar(15)")]
        public string? status { get; set; } = "hiring";

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        public DateTime updatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Companies")]
        [Column("companyId")]
        public int? companyId { get; set; }


        // Navigation Properties
        public Companies? Companies { get; set; }
        public ICollection<Applications>? Applications { get; set; }
        public ICollection<Placements>? Placements { get; set; }
    }

    //public enum type
    //{
    //    fulltime,
    //    parttime,
    //    internship
    //}

    //public enum locationType
    //{
    //    onsite,
    //    remote,
    //    hybrid
    //}

    //public enum VacanciesStatus
    //{
    //    hiring,
    //    hired
    //}
}
