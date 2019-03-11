using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.Symbols
{
	[Serializable()]
	[JsonObject()]
	public class Color : ISymbol
	{


		public Color()
		{
		}

		public Color(IList<int> colorArrary)
		{
            Red = colorArrary[0];
            Green = colorArrary[1];
            Blue = colorArrary[2];
            Alpha = colorArrary[3];
		}

		[JsonIgnore()]
		public int Red { get; set; }

		[JsonIgnore()]
		public int Green { get; set; }

		[JsonIgnore()]
		public int Blue { get; set; }

		[JsonIgnore()]
		public int Alpha { get; set; }

	}
}
