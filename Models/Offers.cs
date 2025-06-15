using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Offers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int offerId { get; set; }

        [Column("annualPackage", TypeName = "varchar(25)")]
        public string? annualPackage { get; set; }

        [Column("joiningDate", TypeName = "varchar(20)")]
        public string? joiningDate { get; set; }

        [Column("timing", TypeName = "varchar(50)")]
        public string? timing { get; set; }

        [Column("position", TypeName = "varchar(50)")]
        public string? position { get; set; }

        [Column("description", TypeName = "varchar(2000)")]
        public string? description { get; set; }

        [Column("offerLetter", TypeName = "varchar(4000)")]
        public string? offerLetter { get; set; }

        [Column("status", TypeName = "varchar(15)")]
        public string? status { get; set; } = "pending";

        [Column("reason", TypeName = "varchar(1000)")]
        public string? reason { get; set; } = null;

        [ForeignKey("applicationId")]
        public int? applicationId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public Applications? Applications { get; set; }
    }

    
    //public enum offerStatus
    //{
    //    pending,
    //    accepted,
    //    rejected
    //}
}
