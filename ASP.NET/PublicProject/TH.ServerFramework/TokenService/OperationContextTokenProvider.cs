// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Net;
using System.ServiceModel.Channels;
using TH.ServerFramework.TokenService;
using TH.ServerFramework.Caching;


namespace TH.ServerFramework
{
	namespace TokenService
	{
		internal class OperationContextTokenProvider : ITokenProvider
		{
			private readonly System.ServiceModel.OperationContext _oc;
			private readonly ICache _cache;
			private readonly TimeSpan _tokenTtl;
            
			
			public OperationContextTokenProvider(System.ServiceModel.OperationContext oc, ICache cache, TimeSpan tokenTtl)
			{
				_oc = oc;
				_cache = cache;
				_tokenTtl = tokenTtl;
			}
			
			private IPEndPoint GetClientIPEndpoint()
			{
				var properties = _oc.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
				var ipEndpoint = new IPEndPoint(IPAddress.Parse(endpoint.Address), endpoint.Port);
				return ipEndpoint;
			}

            //private IPEndPoint GetClientIPEndpoint()
            //{
            //    var properties = _oc.IncomingMessageProperties;
            //    RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //    var ipEndpoint = new IPEndPoint(IPAddress.Parse(endpoint.Address), endpoint.Port);
            //    return ipEndpoint;
            //}

            public AdminTokenDescriptoin CreateToken(UserDescription user)
			{

				var adminToken = new AdminTokenDescriptoin();
				var tokenTtl = _tokenTtl;
				adminToken.ExpiredDate = DateTime.Now + _tokenTtl;
				adminToken.Token = Guid.NewGuid();
				var clientIpEndpoint = GetClientIPEndpoint();
                var tokenVaildInfo = new TokenVaildInfo() { UserLoginName = user.UserLoginName, UserName = user.UserName, UserID = user.UserId, CompanyName= user.CompanyName, DepartmentName= user.DepartmentName, ClientIpEndpoint = clientIpEndpoint };
                _cache.Put((string)(adminToken.Token.ToString()), tokenVaildInfo, tokenTtl);
                adminToken.CompanyName = user.CompanyName;
				return adminToken;
			}
			
			public bool IsTokenVaild(Guid token)
			{
                var tokenVaildInfo = _cache[token.ToString()];
                if (tokenVaildInfo == null)
				{
					return false;
				}
                return true;
                //var currentClientIpEndpoint = GetClientIPEndpoint();
                //if (currentClientIpEndpoint.Address.Equals(tokenVaildInfo.ClientIpEndpoint.Address))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
			}
			
			public DateTime RevokeToken(Guid token)
			{
				var tokenTtl = _tokenTtl;
                var tokenVaildInfo = _cache[token.ToString()] as TokenVaildInfo;
                if(tokenVaildInfo== null)
                    throw (new System.ServiceModel.FaultException<string>("TokenVaild", new System.ServiceModel.FaultReason("认证服务验证失败!")));
				var expired = DateTime.Now + tokenTtl;
                tokenVaildInfo.UpdateTime = DateTime.Now;
                _cache.Put(token.ToString(), tokenVaildInfo, tokenTtl);
				return expired;
			}

            public string GetUserName(Guid token)
            {
                var tokenVaildInfo = _cache[token.ToString()] as TokenVaildInfo;
                if (tokenVaildInfo != null)
                  return   tokenVaildInfo.UserLoginName;
                return null;
            }


            public bool IsTokenVaildFromIPEndPoint(Guid token, IPEndPoint Ip)
            {
                TokenVaildInfo tokenVaildInfo = _cache[token.ToString()] as TokenVaildInfo;
                if (tokenVaildInfo == null)
                {
                    return false;
                }
                if (Ip.Address.Equals(tokenVaildInfo.ClientIpEndpoint.Address))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void QuitToken(Guid token)
            {
                throw new NotImplementedException();
            }

            public AdminTokenDescriptoin CreateToken(UserDescription user, IPEndPoint ipEndPoint)
            {
                var adminToken = new AdminTokenDescriptoin();
                var tokenTtl = _tokenTtl;
                adminToken.ExpiredDate = DateTime.Now + _tokenTtl;
                adminToken.Token = Guid.NewGuid();
                var tokenVaildInfo = new TokenVaildInfo() { UserLoginName = user.UserLoginName, UserName = user.UserName, UserID = user.UserId, CompanyName = user.CompanyName, DepartmentName = user.DepartmentName, ClientIpEndpoint = ipEndPoint };
                _cache.Put((string)(adminToken.Token.ToString()), tokenVaildInfo, tokenTtl);
                return adminToken;
            }


            public TokenVaildInfo GetUserDescription(Guid token)
            {
                var tokenTtl = _tokenTtl;
                var tokenVaildInfo = _cache[token.ToString()] as TokenVaildInfo;
                if (tokenVaildInfo == null)
                    throw (new System.ServiceModel.FaultException<string>("TokenVaild", new System.ServiceModel.FaultReason("认证服务验证失败!")));
                return tokenVaildInfo;
            }

           public string GetClientIp()
           {
              return GetClientIPEndpoint().Address.ToString();
           }
        }
	}
}
