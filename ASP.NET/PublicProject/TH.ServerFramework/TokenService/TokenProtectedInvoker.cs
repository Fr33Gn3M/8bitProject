// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Microsoft.Practices.ServiceLocation;


namespace TH.ServerFramework
{
	namespace TokenService
	{
		public class TokenProtectedInvoker
		{
			
			private readonly ITokenProvider _tokenProvider;
			
			public TokenProtectedInvoker()
			{
				var providerFactory = ServiceLocator.Current.GetInstance<ITokenProviderFactory>();
				_tokenProvider = providerFactory.CreateDefaultProvider();
			}
			
			public TokenProtectedInvoker(ITokenProvider tokenProvider)
			{
				_tokenProvider = tokenProvider;
			}

            public void Invoke(Guid token, Action act)
            {
                if (!_tokenProvider.IsTokenVaild(token))
                    throw (new System.ServiceModel.FaultException<string>("TokenVaild", new System.ServiceModel.FaultReason("认证服务验证失败!")));
                act.Invoke();
            }
		}
	}
}
