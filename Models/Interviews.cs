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
        public string? interviewStatus { get; set; } = "scheduled";

        [Column("reason", TypeName = "varchar(1000)")]
        public string? reason { get; set; } = null;

        [ForeignKey("applicationId")]
        public int? applicationId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("applicationId")]
        [InverseProperty("Interviews")]
        public Applications? Applications { get; set; }
    }

    //public enum interviewStatus
    //{
    //    scheduled,
    //    rescheduled,
    //    completed,
    //    cancelled,
    //    offered
    //}
}
