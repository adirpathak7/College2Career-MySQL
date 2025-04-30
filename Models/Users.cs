using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int usersId { get; set; }

        [Column("email", TypeName = "varchar(50)")]
        public string? email { get; set; }

        [Column("password", TypeName = "varchar(255)")]
        public string? password { get; set; }

        [Column("forgotPasswordToken", TypeName = "varchar(255)")]
        public string? forgotPasswordToken { get; set; } = null;

        [Column("tokenExpirationTime")]
        public DateTime? tokenExpirationTime { get; set; } = null;

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [ForeignKey("roleId")]
        public int? roleId { get; set; }

        // Navigation Properties
        public Roles? Role { get; set; }
        public Companies? Companies { get; set; }
        public Students? Students { get; set; }

    }

    //public enum userRole
    //{
    //    student,
    //    company
    //}
}
