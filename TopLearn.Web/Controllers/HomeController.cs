using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Handlers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
