namespace TH.ServerFramework.WebEndpoint
{
    using TH.ServerFramework.TokenService;
    using TH.ServerFramework.UserManager;
    using System;
    using System.Net.Security;
    using System.ServiceModel;

    [ServiceContract]
    public interface ITokenAdministratorService
    {
        [OperationContract]
        AdminTokenDescriptoin CreateToken(string userName, string password);
        [OperationContract]
        bool IsLogin(string userName);
        [OperationContract]
        void ResetPassword(Guid token, string oldPassword, string newPassword);
        [OperationContract]
        DateTime RevokeToken(Guid token);
        [OperationContract]
        string GetUserName(Guid token);
        //[OperationContract]
        //ModuleInfo[] GetModuleInfos(Guid token);
        [OperationContract]
        void QuitToken(Guid token);
        [OperationContract]
        bool IsLoginSuccess(Guid token);
    
    }
}

