using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Placements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int placementId { get; set; }

        [Column("placementDate", TypeName = "varchar(50)")]
        public string? placementDate { get; set; }

        [Column("salaryPackage", TypeName = "varchar(10)")]
        public string? salaryPackage { get; set; }

        [ForeignKey("studentId")]
        public int? studentId { get; set; }

        [ForeignKey("companyId")]
        public int? companyId { get; set; }

        [ForeignKey("collegeId")]
        public int? collegeId { get; set; }

        [ForeignKey("vacancyId")]
        public int? vacancyId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;


        // Navigation Properties
        [ForeignKey("studentId")]
        [InverseProperty("Placements")]
        public Students? Students { get; set; }

        [ForeignKey("companyId")]
        [InverseProperty("Placements")]
        public Companies? Companies { get; set; }

        [ForeignKey("collegeId")]
        [InverseProperty("Placements")]
        public Colleges? Colleges { get; set; }

        [ForeignKey("vacancyId")]
        [InverseProperty("Placements")]
        public Vacancies? Vacancies { get; set; }

    }

}
