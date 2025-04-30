using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College2Career.Models
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int roleId { get; set; }
        [Column("role", TypeName = "varchar(50)")]
        public string? role { get; set; }

        // Navigation Properties
        public ICollection<Users>? Users { get; set; }
    }
}
