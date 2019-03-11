using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Geometry;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Geometry
{
	[ReflectionProfileMapper()]
	public class LinesMapper : CollectionMapperBase<double[][], Line>
	{
	}
}
