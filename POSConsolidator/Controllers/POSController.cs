using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSConsolidator.Controllers
{
    public class POSController : Controller
    {
        // GET: /POS/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /POS/Sales
        [Authorize]
        public ActionResult Sales()
        {
            return View();
        }

	}
}