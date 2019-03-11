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
using Bootstrap.Extensions.StartupTasks;
using TH.ArcGISServiceManager;


namespace TH.ArcGISServiceManager
{
	namespace Bootstrapper
	{
        public class ArcGISServiceRegister : IStartupTask
		{
			
			public void Reset()
			{
				
			}
			
			public void Run()
			{
                CreateArcGISService.Init();
			}
		}
	}
}
