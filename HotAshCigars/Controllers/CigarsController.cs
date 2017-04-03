using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using X.PagedList;
using X.PagedList.Mvc;

namespace HotAshCigars.Controllers
{
    public class CigarsController : Controller
    {
        // GET: Cigars
        public ActionResult Index()
        {
			using (HotAshContext context = new HotAshContext()) {
				var cigars = context.Cigars;
				return View(cigars.ToPagedList());
			}
        }

        // GET: Cigars/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
       
    } /* end of controller */
}
