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
using System.ServiceModel.Web;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.Utility;
using System.ServiceModel;


namespace TH.ServerFramework
{
	namespace WebService
	{

        
        [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
        public class RIACrossDomainService : IRIACrossDomainService
		{
            private static byte[] FileBytes = null;

            public Stream GetCrossDomainFile()
            {
                var section = SettingsSection.GetSection();
                //if (section.RIACrossDomainService == null || !section.RIACrossDomainService.Enabled)
                //{
                //    WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
                //    return null;
                //}
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
                if (FileBytes == null)
                {
                    var filePath = IOExtensions.SafeGetFullPath(section.RIACrossDomainService.CrossDomainFile);
                    if (!File.Exists((string)filePath))
                    {
                        WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
                        return null;
                    }
                    FileBytes = File.ReadAllBytes((string)filePath);
                }
                var ms = new MemoryStream(FileBytes);
                return ms;
            }
		}
	}
}
