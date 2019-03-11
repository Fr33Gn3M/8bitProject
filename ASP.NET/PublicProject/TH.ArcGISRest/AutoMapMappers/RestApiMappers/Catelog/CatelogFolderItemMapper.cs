using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsCatelogFolderItem = TH.ArcGISRest.ApiImports.CatelogFolderItem;
using DespCatelogFolderItem = TH.ArcGISRest.Description.Catelog.CatelogFolderItem;
using AutoMapper;
using TH.ArcGISRest.Description.Catelog;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.RestApiMappers.Catelog
{
	[ReflectionProfileMapper()]
	public class CatelogFolderItemMapper : ITypeConverter<AgsCatelogFolderItem, DespCatelogFolderItem>
	{

		public DespCatelogFolderItem Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsCatelogFolderItem sourceValue = context.SourceValue as AgsCatelogFolderItem;
			var  targetValue = new DespCatelogFolderItem();
			var _with1 = targetValue;
			_with1.CurrentVersion = sourceValue.CurrentVersionString;
			_with1.FolderNames = sourceValue.FolderNames;
			_with1.ServiceIndexs = Mapper.Map<KeyValuePair<string, ServiceType>[]>(sourceValue.ServiceIndexs);
			return targetValue;
		}
	}
}
