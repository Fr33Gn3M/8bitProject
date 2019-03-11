// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.ServiceModel;
using System.ServiceModel.Web;


namespace TH.ServerFramework
{
	namespace TokenService
	{
		public interface ITokenProviderFactory
		{
			ITokenProvider CreateProvider(WebOperationContext woc);
			ITokenProvider CreateProvider(OperationContext oc);
			ITokenProvider CreateDefaultProvider();
		}
	}
}
