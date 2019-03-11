using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsIdNamePair = TH.ArcGISRest.ApiImports.IdNamePair;
using DespIdNamePair = TH.ArcGISRest.Description.IdNamePair;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers
{
	[ReflectionProfileMapper()]
	public class IdNamePairsMapper : CollectionMapperBase<AgsIdNamePair, DespIdNamePair>
	{
	}
}
