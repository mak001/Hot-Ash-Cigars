using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace HotAshCigars.Models {
	public class OrderDetail {

		[Key]
		public int ID { get; set; }
		[Required]
		public Cigar Cigar { get; set; }
		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public Double PricePaidEach { get; set; }

	}
}