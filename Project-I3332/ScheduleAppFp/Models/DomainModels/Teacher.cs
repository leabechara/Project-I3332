using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ScheduleApplication.Models.DomainModels
{
    public class Teacher
    {
        public Teacher() => Classes = new HashSet<Class>();
        public int TeacherId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        [Required(ErrorMessage = "Please ente a first Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        [Required(ErrorMessage = "Please ente a Last Name")]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
        public ICollection<Class> Classes { get; set; }

    }
}
