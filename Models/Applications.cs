using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace College2Career.Models
{
    public class Applications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int applicationId { get; set; }

        [Column("reason", TypeName = "varchar(300)")]
        public string? reason { get; set; } = null;

        [Column("status", TypeName = "varchar(15)")]
        public string? status { get; set; } = "applied";

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [ForeignKey("Students")]
        [Column("studentId")]
        public int? studentId { get; set; }

        [ForeignKey("Vacancies")]
        [Column("vacancyId")]
        public int? vacancyId { get; set; }


        // Navigation Properties
        public Students? Students { get; set; }
        public Vacancies? Vacancies { get; set; }
        public Interviews? Interviews { get; set; }
        public Offers? Offers { get; set; }
    }

    //public enum applicationStatus
    //{
    //    applied,
    //    rejected,
    //    verified,
    //    interviewScheduled,
    //    offered,
    //    offerAccepted,
    //    offerRejected
    //}
}
