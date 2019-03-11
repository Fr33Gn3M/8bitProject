using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsLabelClass = TH.ArcGISRest.ApiImports.Symbols.LabelClass;
using DespLabelClass = TH.ArcGISRest.Description.Drawing.Label.LabelClass;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class LabelClassesMappers : CollectionMapperBase<DespLabelClass, AgsLabelClass>
	{
	}
}
