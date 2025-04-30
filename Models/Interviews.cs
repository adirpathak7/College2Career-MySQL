using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Interviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int interviewId { get; set; }

        [Column("interviewDate", TypeName = "varchar(50)")]
        public string? interviewDate { get; set; }

        [Column("interviewTime", TypeName = "varchar(50)")]
        public string? interviewTime { get; set; }

        [Column("status", TypeName = "varchar(15)")]
        public string? interviewStatus { get; set; } = "pending";

        [ForeignKey("applicationId")]
        public int? applicationId { get; set; }

        [ForeignKey("collegeId")]
        public int? collegeId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;


        // Navigation Properties
        [ForeignKey("applicationId")]
        [InverseProperty("Interviews")]
        public Applications? Applications { get; set; }

        [ForeignKey("collegeId")]
        [InverseProperty("Interviews")]
        public Colleges? Colleges { get; set; }
    }

    //public enum interviewStatus
    //{
    //    pending,
    //    scheduled,
    //    completed,
    //    cancelled
    //}
}
