using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsCodeValue = TH.ArcGISRest.ApiImports.Domains.CodeValue;
using DespCodeValue = TH.ArcGISRest.Description.Table.CodeValue;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class CodeValuesMapper : CollectionMapperBase<AgsCodeValue, DespCodeValue>
	{
	}
}
