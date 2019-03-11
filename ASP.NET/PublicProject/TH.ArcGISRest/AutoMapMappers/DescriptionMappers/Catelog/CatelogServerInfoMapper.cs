using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.Description.Catelog;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class CatelogServerInfoMapper : ITypeConverter<CatelogServerInfo, ServerInfo>
	{

		public ServerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            CatelogServerInfo sourceValue = context.SourceValue as CatelogServerInfo;
			var  targetValue = new ServerInfo();
			var _with1 = targetValue;
			_with1.CurrentVersionString = sourceValue.CurrentVersion;
			_with1.SecureSoapUrl = sourceValue.SecureSoapUrl.AbsoluteUri;
			_with1.SoapUrl = sourceValue.SoapUrl.AbsoluteUri;
			if (sourceValue.ToekServiceUrl != null) {
				_with1.AuthInfo.TokenBasedSecurity = true;
				_with1.AuthInfo.TokenServiceUrl = sourceValue.ToekServiceUrl.AbsoluteUri;
			} else {
				_with1.AuthInfo.TokenBasedSecurity = false;
			}
			return targetValue;
		}
	}
}
