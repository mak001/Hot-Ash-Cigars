using HotAshCigars.Models;
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
        public ActionResult Index(int page = 1, int pageQty = 15)
        {
			using (HotAshContext context = new HotAshContext()) {
				var cigars = context.Cigars;
				return View(cigars.ToPagedList(page, pageQty));
			}
        }

        // GET: Cigars/Details/5
        public ActionResult Details(int? id)
        {
			if (id == null) {
				return RedirectToAction("Index");
			}

			using (HotAshContext context = new HotAshContext()) {
				Cigar cigar = context.Cigars.FirstOrDefault(x => x.ID == id);
				return View(cigar);
			}
				
        }
       
    } /* end of controller */
}
