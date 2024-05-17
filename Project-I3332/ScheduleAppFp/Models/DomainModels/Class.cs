using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ScheduleApplication.Models.DomainModels
{
    public class Class
    {
        public int ClassId { get; set; }
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters")]
        [Required(ErrorMessage = "please enter class title")]

        public string Title { get; set; } = string.Empty;
        [Range(100, 500, ErrorMessage = "Class number must be between 100 and 500")]

        [Required(ErrorMessage = "please enter class title")]
        public int? Number { get; set; }

        [Display(Name = "Time")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "please enter numbers only for class time")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "classtime must be 4 characters long")]
        [Required(ErrorMessage = "Please Enter a class time(in military time format")]

        public string MilitaryTime { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        [ValidateNever]
        public Teacher Teacher { get; set; }
        public int DayId { get; set; }
        [ValidateNever]
        public Day Day { get; set; } = null;


    }
}
