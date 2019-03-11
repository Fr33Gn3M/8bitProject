using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespDomainBase = TH.ArcGISRest.Description.Table.DomainBase;
using AgsDomainBase = TH.ArcGISRest.ApiImports.Domains.DomainBase;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class DomainBasesMapper : CollectionMapperBase<DespDomainBase, AgsDomainBase>
	{
	}
}
