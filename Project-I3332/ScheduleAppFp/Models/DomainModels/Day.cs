using System.ComponentModel.DataAnnotations;

namespace ScheduleApplication.Models.DomainModels
{
    public class Day
    {
        public Day() => Classes = new HashSet<Class>();
        public int DayId { get; set; }
        [StringLength(10)]
        [Required()]

        public string Name { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}