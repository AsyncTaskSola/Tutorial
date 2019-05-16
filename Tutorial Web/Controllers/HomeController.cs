using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tutorial_Web.Model;
using Tutorial_Web.Services;
using Tutorial_Web.ViewModels;

namespace Tutorial_Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IRepository<Student> _repository;

        public HomeController(IRepository<Student> repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            //            var name= this.ControllerContext.ActionDescriptor.ControllerName;
            // return this.Content("Hello From HomeController");

            #region Check3

            //var vt = new Student()
            //{
            //    Id = 1,
            //    FirstName = "Hex",
            //    LastName = "sola"
            //};
            ////return  new ObjectResult(vt);
            //return View(vt);

            #endregion

            var list = _repository.GetAll();//entity Model=>view model
            var vms = list.Select(x => new StudentViewModel()
            {
                Id = x.Id,
                Name = $"{x.FirstName}{x.LastName}",
                Age = DateTime.Now.Subtract(x.BirthDate).Days / 365
            });
            var vm = new HomeIndexViewModel
            {
                Students = vms
            };
            return View(vm);
        }

        public IActionResult Detail(int id)//路由
        {
            var student = _repository.GetById(id);
            if (student == null)
                return RedirectToAction(nameof(Index));

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken ]
        public IActionResult Create(StudentCenderModel student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student //页面传过来的值，新的内容
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender

                };
                var newMedel = _repository.Add(newStudent);
                return RedirectToAction(nameof(Detail), new { id = newMedel.Id });
            }
            ModelState.AddModelError(string.Empty,"model lever Error!");
           
                return View();


        }
    }
}
