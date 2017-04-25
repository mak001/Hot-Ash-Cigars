using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using X.PagedList;
using X.PagedList.Mvc;

using HotAshCigars.Models;

namespace HotAshCigars.Controllers {

	[Authorize(Roles = "Admin")]
	public class CigarAdminController : Controller {
		// GET: CigarAdmin
		public ActionResult Index(int page = 1, int pageQty = 25) {
			using (HotAshContext context = new HotAshContext()) {
				var cigars = context.Cigars;
				return View(cigars.ToPagedList(page, pageQty));
			}
		}

		// GET: CigarAdmin/Create
		public ActionResult Create() {
			return View();
		}

		// POST: CigarAdmin/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Cigar obj) {
			try {
				using (HotAshContext context = new HotAshContext()) {
					context.Cigars.Add(obj);
					context.SaveChanges();
					return RedirectToAction("Index");
				}
			} catch {
				return View();
			}
		}

		// GET: CigarAdmin/Edit/5
		public ActionResult Edit(int? id) {

			if (id == null) {
				return RedirectToAction("Index");
			}

			Cigar result = null;
			using (HotAshContext context = new HotAshContext()) {
				result = context.Cigars.FirstOrDefault(x => x.ID == id);
			}
			return View(result);
		}

		// POST: CigarAdmin/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Cigar obj) {
			using (HotAshContext context = new HotAshContext()) {
				var cigar = context.Cigars.FirstOrDefault(x => x.ID == id);
				TryUpdateModel(cigar);
				context.SaveChanges();

				return RedirectToAction("Index");
			}
		}

		// GET: CigarAdmin/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return RedirectToAction("Index");
			}

			Cigar result = null;
			using (HotAshContext context = new HotAshContext()) {
				result = context.Cigars.FirstOrDefault(x => x.ID == id);
			}
			return View(result);
		}

		// POST: CigarAdmin/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				using (HotAshContext context = new HotAshContext()) {
					Cigar cigar = context.Cigars.FirstOrDefault(x => x.ID == id);
					context.Cigars.Remove(cigar);
					context.SaveChanges();

					return RedirectToAction("Index");
				}
			} catch {
				return View();
			}
		}
	}
}
