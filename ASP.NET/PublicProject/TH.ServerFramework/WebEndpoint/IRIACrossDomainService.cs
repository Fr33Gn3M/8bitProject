// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace TH.ServerFramework
{
	namespace WebService
	{
		[ServiceContract]public interface IRIACrossDomainService
		{
			[WebGet(UriTemplate = "crossdomain.xml")]
            Stream GetCrossDomainFile();
		}
	}
}
