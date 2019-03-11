// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports


namespace TH.ArcGISRest.Client
{
	public interface ITokenServiceClient : ICommunicationObject
	{
		string GetToken(string userName, string password, TimeSpan ttl);
	}
	
}
