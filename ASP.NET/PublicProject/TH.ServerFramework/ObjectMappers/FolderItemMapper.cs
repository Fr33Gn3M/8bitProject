// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using AutoMapper;
using TH.ServerFramework.IO;


namespace TH.ServerFramework
{
	namespace ObjectMappers
	{
		internal class FolderItemMapper : ITypeConverter<IDirectory, FolderItem>
		{
			
			public FolderItem Convert(ResolutionContext context)
			{
				if (context.IsSourceValueNull)
				{
					return null;
				}
                IDirectory sourceValue = context.SourceValue as IDirectory;
				var targetValue = new FolderItem();
				targetValue.CreatedDate = System.Convert.ToDateTime(sourceValue.CreatedDateUtc);
                targetValue.FullPath = sourceValue.FullPath;
				targetValue.ModifiedDate = System.Convert.ToDateTime(sourceValue.LastModifiedTimeUtc);
				targetValue.Name = sourceValue.Name;
				return targetValue;
			}
		}
	}
}
