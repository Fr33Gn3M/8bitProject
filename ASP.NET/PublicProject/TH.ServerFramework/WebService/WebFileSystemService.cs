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
using Microsoft.Practices.ServiceLocation;
using System.ServiceModel.Web;
using TH.ServerFramework.WebEndpoint;
using TH.ServerFramework.IO;
using TH.ServerFramework.TokenService;
using System.Drawing;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using RestSharp.Contrib;


namespace TH.ServerFramework
{
	namespace WebService
	{
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]    
        [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
        public class WebFileSystemService : IWebFileSystemService
		{
			
			private readonly TokenProtectedInvoker _tokenInvoker;
			
			public WebFileSystemService()
			{
				_tokenInvoker = new TokenProtectedInvoker();
			}
			
            private const string instanceName="img";
			private Stream GetFile(  string fileFullPath)
			{
				var fileSystemRepo = ServiceLocator.Current.GetInstance<IFileSystemProviderRepo>();
				var provider = fileSystemRepo.GetProviderInstance(instanceName);
				if (provider == null)
				{
					WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
					return null;
				}
				var rootDir = provider.GetDirectory(null);
				if (!rootDir.Exists)
				{
					WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
					return null;
				}
				var file = rootDir.GetFile(fileFullPath);
                if (!file.Exists)
				{
					WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
					return null;
				}
                var nOffset = 0; string strOffset = string.Empty, strFetchSize = string.Empty;
                if (!string.IsNullOrEmpty(strOffset))
                {
                    nOffset = int.Parse(strOffset);
                }
                var nFetchSize = file.Size;
                if (!string.IsNullOrEmpty(strFetchSize))
                {
                    nFetchSize = long.Parse(strFetchSize);
                    if (nFetchSize > file.Size)
                    {
                        nFetchSize = file.Size;
                    }
                }
				const int BufferSize = 10240;
				byte[] buffer = new byte[BufferSize - 1 + 1];
				var ms = new MemoryStream();
                using (var fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (nOffset > 0)
					{
						fs.Seek(nOffset, SeekOrigin.Begin);
					}
					do
					{
						var readBytes = fs.Read(buffer, 0, BufferSize);
						if (readBytes <= 0 || ms.Length >= nFetchSize)
						{
							break;
						}
						ms.Write(buffer, 0, readBytes);
					} while (true);
				}
				
				ms.SetLength(System.Convert.ToInt64(nFetchSize));
				ms.Position = 0;
				return ms;
			}
			
			
			public Stream GetFile(string strToken,  string fileFullPath)
			{
                Stream result = null;
                result = GetFile(fileFullPath);
                //Action act = ()=>
                //{
                //};
                //var token = Guid.Parse(strToken);
                //_tokenInvoker.Invoke(token, act);
				return result;
			}


            public void UpdateImages(Stream stream, string imagepath)
            {
                var fileSystemRepo = ServiceLocator.Current.GetInstance<IFileSystemProviderRepo>();
                var provider = fileSystemRepo.GetProviderInstance(instanceName);
                if (provider == null)
                {
                    WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
                }
                var rootDir = provider.GetDirectory(null);
                if (!rootDir.Exists)
                {
                    WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
                }
                var file = rootDir.GetFile(imagepath);
                if (!file.Exists)
                {
                  //  +		stream.GetType()	{Name = "MessageBodyStream" FullName = "System.ServiceModel.Dispatcher.StreamFormatter+MessageBodyStream"}	System.Type {System.RuntimeType}
                    //MessageBodyStream
                    //var queryString = Convert.ToBase64String((stream as MemoryStream).ToArray());
                    //var queryParams2 = HttpUtility.ParseQueryString(queryString);
                    //System.IO.File.WriteAllBytes(file.FullName,);
                    //var bitmap = Bitmap.FromStream(stream);
                    //bitmap.Save(file.FullPath);
                }
            }


            public void UpdateImages(Stream stream, string imagepath, string strToken)
            {
                UpdateImages(stream, imagepath);
                //Action act = () =>
                //{
                //};
                //var token = Guid.Parse(strToken);
                //_tokenInvoker.Invoke(token, act);
       
            }

            public void UpdateImg(string image, string strToken, string imagename)
            {
                if (string.IsNullOrEmpty(image))
                    return;
               var bb  = Convert.FromBase64String(image);
               var stream = new MemoryStream();
               stream.Write(bb, 0, bb.Length);
               stream.Flush();
               UpdateImages(stream, imagename);
            }
        }
	}
}
