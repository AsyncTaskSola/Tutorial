using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial_Web.Model
{
    public class StudentCenderModel
    {
        [Display(Name = "名"), Required]
        public string FirstName { get; set; }

        [Display(Name = "性"), Required, MaxLength(10)]
        public string LastName { get; set; }

        [Display(Name = "生日")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }
    }
}
