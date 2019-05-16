using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial_Web
{
    [Route("about")]
   // [Route("v2/[Controller]/[action]")]
    public class AboutController
    {
        
        public string Me()
        {
            return "Me";
        }
    }
}
