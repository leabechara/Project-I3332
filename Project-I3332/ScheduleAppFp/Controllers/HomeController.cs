
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScheduleAppFp.Models.Data;
using ScheduleApplication.Models;
using ScheduleApplication.Models.Data;
using ScheduleApplication.Models.DomainModels;
using System.Diagnostics;
using System.Linq; 

namespace ScheduleApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly Repository<Class> classes;
        private readonly Repository<Day> days;

        public HomeController(ApplicationDbContext context)
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
            var daylist= days.List(dayOptions);

            ViewBag.Id = id;
            ViewBag.Days = daylist;

            return View(classList);
        }
    }
}
