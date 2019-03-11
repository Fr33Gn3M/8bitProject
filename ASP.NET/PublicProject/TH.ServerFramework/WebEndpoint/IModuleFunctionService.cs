using TH.ServerFramework.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace TH.ServerFramework.WebEndpoint
{
    [ServiceContract]
    public interface IModuleFunctionService
    {
        [OperationContract]
        ModuleInfo[] GetModuleInfos(Guid token);
        [OperationContract]
        void UpdateModuleInfos(Guid token, ModuleInfo[] moduleInfos);
        [OperationContract]
        void DeleteModuleInfos(Guid token, string[] moduleNames);
        [OperationContract]
        void UpdateModuleFunction(Guid token, FunctionInfo[] functions);
        [OperationContract]
        void DeleteModuleFunction(Guid token, string moduleName, string[] functionNames);
        [OperationContract]
        ModuleInfo[] GetModulesFromModuleName(Guid token, string[] moduleNames);
        [OperationContract]
        FunctionInfo[] GetFunctionsFromModule(Guid token, string moduleName);
    }
}
