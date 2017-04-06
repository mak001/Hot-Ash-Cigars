using HotAshCigars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotAshCigars.Controllers
{
    public class CartController : Controller
    {

		private Guid UserID = UserHelper.GetUserID();

		private ShoppingCartSummary GetShoppingCartSummary(HotAshContext context) {
			ShoppingCartSummary summary = new ShoppingCartSummary();
			var cartList = context.ShoppingCarts.Where(x => x.UserID == UserID);
			if (cartList != null && cartList.Count() > 0) {
				summary.TotalValue = cartList.Sum(x => x.Quantity * x.Cigar.Price);
				summary.Quantity = cartList.Sum(x => x.Quantity);
			}
			return summary;
		}

		// GET: SHoppingCart
		public ActionResult Index() {
			using (HotAshContext context = new HotAshContext()) {

				ViewBag.Summary = GetShoppingCartSummary(context);

				var carts = context.ShoppingCarts.Include("Cigar").Where(x => x.UserID == UserID);
				return View(carts.ToList());
			}
		}

		public ActionResult Partial() {
			using (HotAshContext context = new HotAshContext()) {
				ShoppingCartSummary summary = GetShoppingCartSummary(context);
				return PartialView("_AjaxCartSummary", summary);
			}
		}

		public ActionResult AddToCart(int id) {
			using (HotAshContext context = new HotAshContext()) {
				Cigar addCigar = context.Cigars.FirstOrDefault(x => x.ID == id);

				if (addCigar != null) {
					var sameItemInCart = context.ShoppingCarts.FirstOrDefault(x => x.Cigar.ID == id && x.UserID == UserID);
					if (sameItemInCart == null) {
						ShoppingCart sc = new ShoppingCart {
							Cigar = addCigar,
							UserID = UserID,
							Quantity = 1,
							DateAdded = DateTime.Now
						};
						context.ShoppingCarts.Add(sc);
					} else {
						sameItemInCart.Quantity++;
					}

					context.SaveChanges();
				}
				ShoppingCartSummary summary = GetShoppingCartSummary(context);
				return PartialView("_AjaxCartSummary", summary);
			}
		}


		[Authorize]
		[HttpGet]
		public ActionResult Checkout() {
			Guid UserID = UserHelper.GetUserID();
			ViewBag.ApplicationUser = UserHelper.GetApplicationUser();
			ViewBag.CheckingOut = true;

			using (HotAshContext context = new HotAshContext()) {
				var shoppingCartItems = context.ShoppingCarts.Include("Cigar").Where(x => x.UserID == UserID);

				Order newOrder = new Order {
					OrderDate = DateTime.Now,
					UserID = UserID,
					OrderDetails = new List<OrderDetail>()
				};

				foreach (var item in shoppingCartItems) {
					OrderDetail od = new OrderDetail {
						Cigar = item.Cigar,
						PricePaidEach = item.Cigar.Price,
						Quantity = item.Quantity
					};
					newOrder.OrderDetails.Add(od);
				}
				return View("Details", newOrder);
			}
		}

		[Authorize]
		[HttpPost]
		public ActionResult Checkout(Order order) {
			Guid UserID = UserHelper.GetUserID();
			ViewBag.ApplicationUser = UserHelper.GetApplicationUser();

			using (HotAshContext context = new HotAshContext()) {
				var shoppingCartCigars = context.ShoppingCarts.Include("Cigar").Where(x => x.UserID == UserID);
				order.OrderDetails = new List<OrderDetail>();

				order.UserID = UserID;
				order.OrderDate = DateTime.Now;

				foreach (var Cigar in shoppingCartCigars) {
					int quantity = 0;
					int.TryParse(Request.Form.Get(Cigar.Cigar.ID.ToString()), out quantity);

					if (quantity > 0) {
						OrderDetail od = new OrderDetail {
							Cigar = Cigar.Cigar,
							PricePaidEach = Cigar.Cigar.Price,
							Quantity = quantity
						};
						order.OrderDetails.Add(od);
					}
				}

				order = context.Orders.Add(order);
				context.ShoppingCarts.RemoveRange(shoppingCartCigars);
				context.SaveChanges();

				return RedirectToAction("Details", "Order", new { id = order.ID });
			}
		}
	}
}