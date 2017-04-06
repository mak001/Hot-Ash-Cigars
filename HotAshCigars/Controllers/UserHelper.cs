using HotAshCigars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HotAshCigars.Controllers {
	public class UserHelper {

		private const string cookieName = "HotAshTemporaryUserCookie";

		public static Guid GetUserID() {
			Guid userId;

			if (HttpContext.Current.User != null) {
				string userid = HttpContext.Current.User.Identity.GetUserId();
				if (Guid.TryParse(userid, out userId)) {
					return userId;
				}
			}

			if (HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null) {
				HttpCookie tempUserCookie = HttpContext.Current.Request.Cookies.Get(cookieName);

				if (tempUserCookie != null && Guid.TryParse(tempUserCookie.Value, out userId)) {
					return userId;
				}
			}

			userId = Guid.NewGuid();
			HttpContext.Current.Response.Cookies.Add(
				new HttpCookie(cookieName, userId.ToString())
			);
			HttpContext.Current.Request.Cookies.Add(
				new HttpCookie(cookieName, userId.ToString())
			);

			return userId;
		}

		public static ApplicationUser GetApplicationUser() {
			string userId = HttpContext.Current.User.Identity.GetUserId();
			ApplicationUserManager aum = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

			return aum.FindById(userId);
		}

		public static void TransferTemporaryUserToRealUser(Guid tempId, string userId) {

			using (HotAshContext context = new HotAshContext()) {
				if (context.ShoppingCarts.Any(x => x.UserID == tempId)) {
					Guid newUserID = Guid.Parse(userId);
					var list = context.ShoppingCarts.Include("Cigar").Where(x => x.UserID == tempId);

					foreach (var tempCart in list) {
						var sameItemInCart = context.ShoppingCarts
							.FirstOrDefault(x => x.Cigar.ID == tempCart.Cigar.ID && x.UserID == newUserID);

						if (sameItemInCart == null) {
							tempCart.UserID = newUserID;
						} else {
							sameItemInCart.Quantity++;
							context.ShoppingCarts.Remove(tempCart);
						}
					}

					context.SaveChanges();
				}
			}
		}


	}
}