using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.Description.Catelog;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.RestApiMappers.Catelog
{
	[ReflectionProfileMapper()]
	public class ServerInfoMapper : ITypeConverter<ServerInfo, CatelogServerInfo>
	{

		public CatelogServerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            ServerInfo sourceValue = context.SourceValue as ServerInfo;
			var  targetValue = new CatelogServerInfo();
			var _with1 = targetValue;
			_with1.CurrentVersion = sourceValue.CurrentVersionString;
			if (!string.IsNullOrEmpty(sourceValue.SecureSoapUrl)) {
				_with1.SecureSoapUrl = new Uri(sourceValue.SecureSoapUrl);
			}
			if (!string.IsNullOrEmpty(sourceValue.SoapUrl)) {
				_with1.SoapUrl = new Uri(sourceValue.SoapUrl);
			}
			if (sourceValue.AuthInfo != null && sourceValue.AuthInfo.TokenBasedSecurity) {
				_with1.ToekServiceUrl = new Uri(sourceValue.AuthInfo.TokenServiceUrl);
			}
			return targetValue;
		}
	}
}
