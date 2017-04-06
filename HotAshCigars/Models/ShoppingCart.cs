using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace HotAshCigars.Models {
	public class ShoppingCart {

		[Key]
		public int ID { get; set; }

		[Required]
		public Cigar Cigar { get; set; }

		[Required]
		public Guid UserID { get; internal set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }

		[Required]
		public DateTime DateAdded { get; set; }

	}
}