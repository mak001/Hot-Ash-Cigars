using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotAshCigars.Models {
	public class Dimension {

		public float Width { get; set; }
		public float Height { get; set; }
		public float Depth { get; set; }

		public override string ToString() {
			return Width + " X " + Height + " X " + Depth;
		}
	}
}