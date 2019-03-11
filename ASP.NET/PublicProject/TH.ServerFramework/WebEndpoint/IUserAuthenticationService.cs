using TH.ServerFramework.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace TH.ServerFramework.WebEndpoint
{
    [ServiceContract]
    public interface IUserAuthenticationService
    {

        [OperationContract]
        void SetRoleFunction(Guid token,string roleName, FunctionInfo[] functions);
        [OperationContract]
        void SetUserRole(Guid token, string userLoginName, RoleInfo[] roles);
        [OperationContract]
        DepartmentInfo[] GetDepartmentInfos(Guid token);
        [OperationContract]
        void DeleteDepartment(Guid token, string departmentName);
        [OperationContract]
        void UpdateDepartment(Guid token, DepartmentInfo[] departments);
        [OperationContract]
        UserInfo[] GetDepartmentUserInfos(Guid token, string departmentName);
        [OperationContract]
        void UpdateDepartmentUserInfos(Guid token, UserInfo[] userInfos);
        [OperationContract]
        void DeleteDepartmentUserInfos(Guid token, string[] userLoginNames);

        [OperationContract]
        RoleInfo[] GetRoleInfos(Guid token);
        [OperationContract]
        void UpdateRoleInfos(Guid token, RoleInfo[] roleInfos);
        [OperationContract]
        void DeleteRoleInfos(Guid token, string[] roleNames);
        [OperationContract]
        ModuleInfo[] GetModulesFromUser(Guid token, string userLoginName);
        //[OperationContract]
        //FunctionInfo[] GetFunctionsFromModule(Guid token, string moduleName);
        //[OperationContract]
        //ModuleInfo[] GetModuleInfos(Guid token);
        //[OperationContract]
        //void UpdateModuleInfos(Guid token, ModuleInfo[] moduleInfos);
        //[OperationContract]
        //void DeleteModuleInfos(Guid token, string[] moduleNames);
        //[OperationContract]
        //void UpdateModuleFunction(Guid token,FunctionInfo[] functions);
        //[OperationContract]
        //void DeleteModuleFunction(Guid token, string moduleName, string[] functionNames);
        //[OperationContract]
        //ModuleInfo[] GetModulesFromModuleName(Guid token, string[] moduleNames);
        [OperationContract]
        RoleInfo[] GetRoleInfosFromRoleName(Guid token, string[] roleNames);

        [OperationContract]
        RoleInfo[] GetRoleInfosFromUserName(Guid token, string userLoginNames);

        [OperationContract]
        bool IsUserNameExist(Guid token, string userName);
        [OperationContract]
        bool IsUserLoginNameExist(Guid token, string userLoginName);
        [OperationContract]
        void ReSetPassword(Guid token,string userLoginName);
        [OperationContract]
        ModuleInfo[] GetModuleInfos(Guid token);
        [OperationContract]
        string GetModuleServiceUrl(Guid token, string moduleName);
        [OperationContract]
        FunctionInfo[] GetFunctionInfos(Guid token, string moduleName);
        [OperationContract]
        FunctionInfo[] GetFunctionsFromRole(Guid token, string roleName);

    }
}
