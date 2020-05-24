using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperExamples.Models
{
    public class AppUserDto
    {
        [Key]
        [Column("Id")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }

        [Column("FirstName")]
        [Required]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Column("LastName")]
        [Required]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Column("Age")]
        [Required]
        [Display(Name = "Age")]
        public virtual int Age { get; set; }
    }
}
