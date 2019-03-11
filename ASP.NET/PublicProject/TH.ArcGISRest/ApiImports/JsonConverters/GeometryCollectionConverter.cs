using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class GeometryCollectionConverter : CollectionConverterBase<AgsGeometryBase>
	{

		protected override JsonConverter CreateElemConverter()
		{
			return new GeomertyConverter();
		}
	}
}
