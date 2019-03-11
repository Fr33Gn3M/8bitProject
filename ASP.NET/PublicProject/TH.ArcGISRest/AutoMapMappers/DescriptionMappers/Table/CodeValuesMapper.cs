using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespCodeValue = TH.ArcGISRest.Description.Table.CodeValue;
using AgsCodeValue = TH.ArcGISRest.ApiImports.Domains.CodeValue;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class CodeValuesMapper : CollectionMapperBase<DespCodeValue, AgsCodeValue>
	{
	}
}
