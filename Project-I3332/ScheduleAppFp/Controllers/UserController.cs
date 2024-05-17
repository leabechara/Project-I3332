using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleApplication.Models.Data;
using ScheduleApplication.Models.DomainModels;

namespace ScheduleApplication.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly Repository<Class> classes;
        private readonly Repository<Day> days;

        public UserController(ScheduleAppFp.Models.Data.ApplicationDbContext context)
        {
            classes = new Repository<Class>(context);
            days = new Repository<Day>(context);
        }

        public IActionResult Index(int id)
        {
            var dayOptions = new QueryOptions<Day>
            {
                OrderBy = d => d.DayId
            };

            var classOptions = new QueryOptions<Class>
            {
                Includes = "Teacher, Day"
            };

            if (id == 0)
            {
                classOptions.OrderBy = c => c.DayId;
                classOptions.ThenOrderBy = c => c.MilitaryTime;
            }
            else
            {
                classOptions.Where = c => c.DayId == id;
                classOptions.ThenOrderBy = c => c.MilitaryTime;
            }

            var classList = classes.List(classOptions);
            var daylist = days.List(dayOptions);

            ViewBag.Id = id;
            ViewBag.Days = daylist;

            return View(classList);
        }

        [HttpGet] // Add this attribute
        public IActionResult View(int id)
        {
            var c = this.GetClass(id);
            return View(c);
        }

        private Class GetClass(int id)
        {
            var classOptions = new QueryOptions<Class>
            {
                Includes = "Teacher,Day",
                Where = c => c.ClassId == id
            };
            return classes.Get(classOptions) ?? new Class();
        }
    }
}
