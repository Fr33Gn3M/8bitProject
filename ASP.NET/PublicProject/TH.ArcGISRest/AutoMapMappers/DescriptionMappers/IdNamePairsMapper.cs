using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespIdNamePair = TH.ArcGISRest.Description.IdNamePair;
using AgsIdNamePair = TH.ArcGISRest.ApiImports.IdNamePair;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers
{
	[ReflectionProfileMapper()]
	public class IdNamePairsMapper : CollectionMapperBase<DespIdNamePair, AgsIdNamePair>
	{
	}
}
