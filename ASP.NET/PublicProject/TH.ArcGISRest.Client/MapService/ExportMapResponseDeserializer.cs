// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.MapServices;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	public class ExportMapResponseDeserializer : JsonDeserializer
	{
		
		public override T DeserializeObject<T>(byte[] content)
		{
			object result = null;
			if (this.RequestParams["f"].Equals("image", StringComparison.OrdinalIgnoreCase))
			{
				result = content;
			}
			else if (this.RequestParams["f"].Equals("json", StringComparison.OrdinalIgnoreCase))
			{
				result = base.DeserializeObject<AgsExportMapParams>(content);
			}
			else
			{
				throw (new NotSupportedException(this.RequestParams["f"]));
			}
			return (T)result;
		}
	}
	
}
