using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScheduleApplication.Models.Data;
using ScheduleApplication.Models.DomainModels;

namespace ScheduleApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private Repository<Teacher> teachers { get; set; }

        public TeacherController(ScheduleAppFp.Models.Data.ApplicationDbContext ctx)
        {
            teachers = new Repository<Teacher>(ctx);
        }

        public IActionResult Index()
        {
            var options = new QueryOptions<Teacher>
            {
                OrderBy = t => t.LastName
            };
            return View(teachers.List(options));
        }
        [HttpGet]
        public ViewResult add() => View();
        [HttpPost]
        public IActionResult Add(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teachers.Insert(teacher);
                teachers.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(teacher);
            }
        }
        [HttpGet]
        public ViewResult Delete(int id) => View(teachers.Get(id));
        [HttpPost]
        public RedirectToActionResult Delete(Teacher teacher)
        {
            teachers.Delete(teacher);
            teachers.Save();
            return RedirectToAction("Index");
        }
    }
}
