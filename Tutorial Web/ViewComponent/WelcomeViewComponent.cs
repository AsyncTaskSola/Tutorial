using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Tutorial_Web.Model;
using Tutorial_Web.Services;


namespace Tutorial_Web.ViewComponent
{
    public class WelcomeViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IRepository<Student> _repository;

        public WelcomeViewComponent(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var count = _repository.GetAll().Count().ToString();
            return View("Default",count);
        }

    }
}
