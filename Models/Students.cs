using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace College2Career.Models
{
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int studentId { get; set; }

        [Column("studentName", TypeName = "varchar(50)")]
        public string? studentName { get; set; }

        [Column("rollNumber", TypeName = "varchar(15)")]
        public string? rollNumber { get; set; }

        [Column("course", TypeName = "varchar(25)")]
        public string? course { get; set; }

        [Column("graduationYear", TypeName = "varchar(15)")]
        public string? graduationYear { get; set; }

        [Column("resume", TypeName = "varchar(1000)")]
        public string? resume { get; set; }

        [Column("status", TypeName = "varchar(15)")]
        public string? status { get; set; } = "pending";

        [Column("statusReason", TypeName = "varchar(1000)")]
        public string? statusReason { get; set; }

        [ForeignKey("usersId")]
        public int? usersId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;


        // Navigation Properties
        public Users? Users { get; set; }
        public ICollection<Applications>? Applications { get; set; }
        public ICollection<Placements>? Placements { get; set; }
        public ICollection<Feedbacks>? Feedbacks { get; set; }
    }

    //public enum isStudentVerified
    //{
    //    pending,
    //    activated,
    //    rejected,
    //    deactivated
    //}

}
