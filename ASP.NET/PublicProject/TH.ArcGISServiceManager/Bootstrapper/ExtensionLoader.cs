// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Microsoft.Practices.ServiceLocation;


namespace TH.ArcGISServiceManager
{
	namespace Bootstrapper
	{
		internal class ExtensionLoader : PH.ServerFramework.Extensions.IExtension
		{
			
			public void OnLoad(IDictionary<string, string> arguments, IServiceLocator serviceLocator)
			{
				
			}
			
			public void OnUnload()
			{
				
			}
		}
	}
}
