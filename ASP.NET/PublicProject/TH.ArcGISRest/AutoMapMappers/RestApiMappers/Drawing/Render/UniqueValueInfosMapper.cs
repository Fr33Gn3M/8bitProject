using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsUniqueValueInfo = TH.ArcGISRest.ApiImports.Renderers.UniqueValueInfo;
using DespUniqueValueInfo = TH.ArcGISRest.Description.Drawing.Render.UniqueValueInfo;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class UniqueValueInfosMapper : CollectionMapperBase<AgsUniqueValueInfo, DespUniqueValueInfo>
	{

	}
}
