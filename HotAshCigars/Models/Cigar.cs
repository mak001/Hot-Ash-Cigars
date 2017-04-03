using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotAshCigars.Models {
	public class Cigar {

		[Key]
		public int ID { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public float Weight { get; set; }
		public Dimension Dimensions { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public float Price { get; set; }

	} /* end of class */
}