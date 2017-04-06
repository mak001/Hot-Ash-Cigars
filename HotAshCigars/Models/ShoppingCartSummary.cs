using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace HotAshCigars.Models {
	public class ShoppingCartSummary {

		public int Quantity { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public double TotalValue { get; set; }

	}
}