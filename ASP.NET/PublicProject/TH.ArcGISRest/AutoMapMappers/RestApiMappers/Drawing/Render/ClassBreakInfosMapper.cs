using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsClassBreakInfo = TH.ArcGISRest.ApiImports.Renderers.ClassBreakInfo;
using DespClassBreakInfo = TH.ArcGISRest.Description.Drawing.Render.ClassBreakInfo;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class ClassBreakInfosMapper : CollectionMapperBase<AgsClassBreakInfo, DespClassBreakInfo>
	{
	}
}
