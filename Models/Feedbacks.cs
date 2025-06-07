using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Feedbacks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int feedbackId { get; set; }

        [Column("comments", TypeName = "varchar(1000)")]
        public string? comments { get; set; }

        [Column("rating", TypeName = "varchar(10)")]
        public string? rating { get; set; }

        public int? studentId { get; set; }
        public int? companyId { get; set; }
        public int? collegeId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;


        // Navigation Properties
        public Students Students { get; set; }
        public Companies Companies { get; set; }
        public Colleges Colleges { get; set; }
    }

}
