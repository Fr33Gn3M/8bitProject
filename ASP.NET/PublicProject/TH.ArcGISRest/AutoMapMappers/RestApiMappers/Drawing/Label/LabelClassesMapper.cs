using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsLabelClass = TH.ArcGISRest.ApiImports.Symbols.LabelClass;
using DespLabelClass = TH.ArcGISRest.Description.Drawing.Label.LabelClass;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Label
{
	[ReflectionProfileMapper()]
	public class LabelClassesMapper : CollectionMapperBase<AgsLabelClass, DespLabelClass>
	{
	}
}
