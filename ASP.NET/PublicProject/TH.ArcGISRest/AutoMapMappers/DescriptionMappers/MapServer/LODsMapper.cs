using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsLod = TH.ArcGISRest.ApiImports.MapServices.Lod;
using DespLod = TH.ArcGISRest.Description.MapServer.LOD;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class LODsMapper : CollectionMapperBase<DespLod, AgsLod>
	{
	}
}
