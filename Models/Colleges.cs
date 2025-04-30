using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace College2Career.Models
{
    public class Colleges
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collegeId { get; set; }

        [Column("collegeName", TypeName = "varchar(50)")]
        public string? collegeName { get; set; }

        [Column("establishedDate", TypeName = "varchar(50)")]
        public string? establishedDate { get; set; }

        [Column("contactNumber", TypeName = "varchar(50)")]
        public string? contactNumber { get; set; }

        [Column("profilePicture", TypeName = "varchar(500)")]
        public string? profilePicture { get; set; }

        [Column("area", TypeName = "varchar(50)")]
        public string? area { get; set; }

        [Column("city", TypeName = "varchar(50)")]
        public string? city { get; set; }

        [Column("state", TypeName = "varchar(50)")]
        public string? state { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;


        // Navigation Properties
        public ICollection<Interviews>? Interviews { get; set; }
        public ICollection<Placements>? Placements { get; set; }
        public ICollection<Feedbacks>? Feedbacks { get; set; }
    }

}
