using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace HotAshCigars.Models {
	public class Order {

		[Key]
		public int ID { get; set; }

		[Required]
		public Guid UserID { get; set; }

		public DateTime OrderDate { get; set; }
		public string HowPaid { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
		public double DiscountAmount { get; set; }

	}
}