using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Offers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int offerId { get; set; }

        [Column("offerLetter", TypeName = "varchar(1000)")]
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

        //[Column("salary", TypeName = "varchar(100)")]
        //public string? salary { get; set; }

        //[Column("joiningDate")]
        //public DateTime? joiningDate { get; set; }

        //[Column("position", TypeName = "varchar(100)")]
        //public string? position { get; set; }


        //public enum offerStatus
        //{
        //    pending,
        //    accepted,
        //    rejected
        //}
    }
