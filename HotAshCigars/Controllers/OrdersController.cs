using HotAshCigars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using X.PagedList;

namespace HotAshCigars.Controllers
{
	[Authorize]
	public class OrdersController : Controller
    {
		public ActionResult Index(int page = 1, int pageSize = 15) {
			ViewBag.ApplicationUser = UserHelper.GetApplicationUser();

			using (HotAshContext context = new HotAshContext()) {
				var UserId = UserHelper.GetUserID();
				IPagedList<Order> orders = context.Orders.ToPagedList(page, pageSize);

				if (!User.IsInRole("Admin")) {
					orders = context.Orders.Where(x => x.UserID == UserId).ToPagedList(page, pageSize);
				}

				return View(orders);
			}
		}

		public ActionResult Details(int? id) {
			if (id == null) {
				return this.RedirectToAction("Index");
			}

			Guid UserID = UserHelper.GetUserID();
			ViewBag.ApplicationUser = UserHelper.GetApplicationUser();

			using (HotAshContext context = new HotAshContext()) {
				IQueryable<Order> orders = context.Orders.Include(p => p.OrderDetails.Select(c => c.Cigar));

				Order order;

				if (User.IsInRole("Admin")) {
					order = orders.FirstOrDefault(x => x.ID == id);
				} else {
					order = orders.FirstOrDefault(x => x.ID == id && x.UserID == UserID);
				}

				if (order == null) {
					return this.RedirectToAction("Index");
				}

				return View(order);
			}
		}
	}
}