namespace TH.ServerFramework.WebEndpoint
{
    using TH.ServerFramework.TokenService;
    using TH.ServerFramework.UserManager;
    using System;
    using System.Net.Security;
    using System.ServiceModel;

    [ServiceContract]
    public interface IProxyTokenAdministratorService
    {
        [OperationContract]
        AdminTokenDescriptoin CreateToken(VaildFunctionInfo vaildFunctionInfo,string userName, string password);
        [OperationContract]
        void ResetPassword(Guid token, string oldPassword, string newPassword);
        [OperationContract]
        DateTime RevokeToken(Guid token);
        [OperationContract]
        string GetModuleServiceUrl(Guid token, string moduleName);
        [OperationContract]
        string GetUserName(Guid token);
        [OperationContract]
        ModuleInfo[] GetModuleInfos(Guid token);
        [OperationContract]
        void QuitToken(Guid token);
        [OperationContract]
        FunctionInfo[] GetFunctionInfos(Guid token, string moduleName);
    }
}

