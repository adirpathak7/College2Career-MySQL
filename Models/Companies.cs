using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace College2Career.Models
{
    public class Companies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int companyId { get; set; }

        [Column("companyName", TypeName = "varchar(50)")]
        public string? companyName { get; set; }

        [Column("establishedDate", TypeName = "varchar(50)")]
        public string? establishedDate { get; set; }

        [Column("contactNumber", TypeName = "varchar(50)")]
        public string? contactNumber { get; set; }

        [Column("profilePicture", TypeName = "varchar(1000)")]
        public string? profilePicture { get; set; }

        [Column("industry", TypeName = "varchar(50)")]
        public string? industry { get; set; }

        [Column("address", TypeName = "varchar(350)")]
        public string? address { get; set; }

        [Column("city", TypeName = "varchar(50)")]
        public string? city { get; set; }

        [Column("state", TypeName = "varchar(50)")]
        public string? state { get; set; }

        [Column("employeeSize", TypeName = "varchar(10)")]
        public string? employeeSize { get; set; }

        [Column("reasonOfStatus", TypeName = "varchar(1000)")]
        public string? reasonOfStatus { get; set; } = null;

        [Column("status", TypeName = "varchar(20)")]
        public string? status { get; set; } = "pending";

        [ForeignKey("usersId")]
        public int? usersId { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;


        // Navigation Properties
        public Users? Users { get; set; }
        public ICollection<Vacancies>? Vacancies { get; set; }
        public ICollection<Placements>? Placements { get; set; }
        public ICollection<Feedbacks>? Feedbacks { get; set; }

        //public enum companiesStatus
        //{
        //    pending,
        //    activated,
        //    rejected,
        //    deactivated
        //}

    }
}
