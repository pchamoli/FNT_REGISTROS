using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Seguridad.PRODUCE;
using System.Web.Routing;

namespace MERCADOS.Web.Controllers
{
   // [Authorize]
   // [Autorizacion]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}