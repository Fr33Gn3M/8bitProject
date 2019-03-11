// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Runtime.Serialization;


namespace TH.ServerFramework
{
	namespace TokenService
	{
		[Serializable]
		[DataContract(Namespace = Namespaces.THArcGISRest)]
		public class AdminTokenDescriptoin
		{
            [DataMember]
            public Guid Token { get; set; }
			[DataMember]
            public DateTime ExpiredDate {get; set;}
            [DataMember]
            public string CompanyName { get; set; }
        }
	}
}
