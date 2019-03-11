// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Bootstrap.Extensions.StartupTasks;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	public class RegisterClientProfilesTask : IStartupTask
	{
		
		public void Reset()
		{
			
		}
		
		private static void RegisterProfile<T>() where T : IMyRestClientProfile
		{
			var profile = Activator.CreateInstance<T>();
			MyRestClientFactory.RegisterProfile(profile);
		}

        public void Run()
		{
			RegisterProfile<CatelogServiceClientProfile>();
			RegisterProfile<MapServiceClientProfile>();
			RegisterProfile<FeatureServiceClientProfile>();
			RegisterProfile<GeometryServiceClientProfile>();
		}
	}
	
}
