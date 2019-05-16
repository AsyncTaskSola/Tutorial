using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial_Web.ViewModels
{
    public class HomeIndexViewModel
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int Age { get; set; }


        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}
