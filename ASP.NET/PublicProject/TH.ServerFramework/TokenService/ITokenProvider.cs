// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
using System.Net;
// End of VB project level imports


namespace TH.ServerFramework
{
	namespace TokenService
	{
		public interface ITokenProvider
		{
            AdminTokenDescriptoin CreateToken(UserDescription user);
            AdminTokenDescriptoin CreateToken(UserDescription user, IPEndPoint ipEndPoint);
			bool IsTokenVaild(Guid token);
			DateTime RevokeToken(Guid token);
            TokenVaildInfo GetUserDescription(Guid token);
            void QuitToken(Guid token);
            bool IsTokenVaildFromIPEndPoint(Guid token,IPEndPoint Ip);
            string GetClientIp();
        }
	}
}
