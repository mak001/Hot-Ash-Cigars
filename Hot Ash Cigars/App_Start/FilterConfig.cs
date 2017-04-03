using System.Web;
using System.Web.Mvc;

namespace Hot_Ash_Cigars {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
