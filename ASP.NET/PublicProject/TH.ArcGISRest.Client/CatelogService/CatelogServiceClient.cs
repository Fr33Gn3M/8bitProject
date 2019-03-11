// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	public class CatelogServiceClient : ICatelogServiceClient
	{
		
		private readonly Uri _baseUrl;
		private readonly string _profileName;
		
		public CatelogServiceClient(Uri baseUrl, string profileName = null)
		{
			_baseUrl = baseUrl;
			_profileName = profileName;
		}
		
		private string GetProfileName()
		{
			return _profileName;
		}
		
		public CatelogFolderItem GetFolderInfo(string folderPath)
		{
			var resource = ServiceResources.Folder;
			if (!string.IsNullOrWhiteSpace(folderPath))
			{
				resource += "/" + folderPath.Trim().Trim('/');
			}
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				var result = restClient.Request<CatelogFolderItem>(resource);
				return result;
			}
			
		}
		
		public ServerInfo GetServerInfo()
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				var result = restClient.Request<ServerInfo>(ServiceResources.ServerInfo);
				return result;
			}
			
		}

        public ServerInfo GetServerDetail()
        {
            using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
            {
                var result = restClient.Request<ServerInfo>(null);
                return result;
            }

        }
		
		public void Close()
		{
		}
		
		public void Open()
		{
		}
		
		
		public System.Uri Via
		{
			get
			{
				return _baseUrl;
			}
			set
			{
				throw (new NotSupportedException());
			}
		}
		
#region IDisposable Support
		private bool disposedValue; // To detect redundant calls
		
		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}
				
				// TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
				// TODO: set large fields to null.
			}
			this.disposedValue = true;
		}
		
		// TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
		//Protected Overrides Sub Finalize()
		//    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		//    Dispose(False)
		//    MyBase.Finalize()
		//End Sub
		
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
#endregion
		
		private class ServiceResources
		{
			public const string ServerInfo = "info";
			public const string Folder = "services";
		}
		
		public TimeSpan Timeout {get; set;}
	}
	
}
