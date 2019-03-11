using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsCatelogFolderItem = TH.ArcGISRest.ApiImports.CatelogFolderItem;
using DespCatelogFolderItem = TH.ArcGISRest.Description.Catelog.CatelogFolderItem;
using AutoMapper;
using TH.ArcGISRest.ApiImports;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class CatelogFolderItemMapper : ITypeConverter<DespCatelogFolderItem, AgsCatelogFolderItem>
	{

		public AgsCatelogFolderItem Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespCatelogFolderItem sourceValue = context.SourceValue as DespCatelogFolderItem;
			var  targetValue = new AgsCatelogFolderItem();
			var _with1 = targetValue;
			_with1.CurrentVersionString = sourceValue.CurrentVersion;
			_with1.FolderNames = sourceValue.FolderNames;
			_with1.ServiceIndexs = Mapper.Map<NameTypePair[]>(sourceValue.ServiceIndexs);
			return targetValue;
		}
	}
}
