using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Offers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int offerId { get; set; }

        [Column("offerLater", TypeName = "varchar(1000)")]
        public string? offerLater { get; set; }

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
