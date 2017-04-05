using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotAshCigars {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Cigar Admin",
				url: "Admin/Cigars/{action}/{id}",
				defaults: new {
					controller = "CigarAdmin", 
					action = "Index",
					id = UrlParameter.Optional
				}
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Cigars", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
