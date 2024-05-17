﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppFp.Data;
using ScheduleAppFp.Models.Data;
using ScheduleApplication.Models.Data;
using ScheduleApplication.Models.DomainModels;

namespace ScheduleApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassController : Controller
    {
        private Repository<Class> classes { get; set; }
        private Repository<Teacher> teachers { get; set; }
        private Repository<Day> days { get; set; }

        public ClassController(ApplicationDbContext ctx)
        {
            classes = new Repository<Class>(ctx);
            teachers = new Repository<Teacher>(ctx);
            days = new Repository<Day>(ctx);
        }

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            this.LoadViewBag("Add");
            return View("AddEdit", new Class());
        }

        [HttpPost]
        public IActionResult Add(Class c)
        {
            bool isAdd = c.ClassId == 0;
            if (ModelState.IsValid)
            {
                if (isAdd)
                {
                    classes.Insert(c);
                }
                else
                {
                    classes.Update(c);
                }

                classes.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string operation = (isAdd) ? "Add" : "Edit";
                this.LoadViewBag(operation);
                return View("AddEdit", c);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var c = this.GetClass(id);
            return View("AddEdit", c);
        }

        [HttpGet] 
        public IActionResult View(int id)
        {
            var c = this.GetClass(id);
            return View(c);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var c = this.GetClass(id);
            return View(c);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Class c)
        {
            classes.Delete(c);
            classes.Save();
            return RedirectToAction("Index", "Home");
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

        private void LoadViewBag(string operation)
        {
            ViewBag.Days = days.List(new QueryOptions<Day>
            {
                OrderBy = d => d.DayId
            });
            ViewBag.Teachers = teachers.List(new QueryOptions<Teacher>
            {
                OrderBy = t => t.LastName
            });
            ViewBag.Operation = operation;
        }
    }
}
