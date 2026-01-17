using LX.Commons.ExceptionManager;
using LX.FrameWork.SystemManager.Interfaces;
using LX.WebCommons.Models;
using Sys.DataBase;

namespace LX.FrameWork.SystemManager.Impls
{
    public class DataServiceRepo : IDataServiceRepo
    {

        private ISystemAppContext SystemAppContext;

        public DataServiceRepo(ISystemAppContext systemAppContext) 
        {
            SystemAppContext = systemAppContext;
        }

        public JsonResults QueryNoToken(QueryPageFilter filter)
        {
            
            int count = 0;
            var result = new JsonResults() { };

            result.Data = SystemAppContext.DataBaseClassHelper().GetQueryResultN(filter, ref count);
            result.Total = count;

            return result;

        }
    }
}
