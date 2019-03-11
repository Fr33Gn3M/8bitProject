// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports;


namespace TH.ArcGISRest.Client
{
	public interface ICatelogServiceClient : ICommunicationObject
	{
		ServerInfo GetServerInfo();
		CatelogFolderItem GetFolderInfo(string folderPath);
        ServerInfo GetServerDetail();
	}
	
}
