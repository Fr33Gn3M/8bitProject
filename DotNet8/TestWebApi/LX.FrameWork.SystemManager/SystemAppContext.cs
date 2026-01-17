using Sys.DataBase;

namespace LX.FrameWork.SystemManager
{
    public class SystemAppContext : ISystemAppContext
    { 

        public IDataClassHelper dataBaseClassHelper;

        public SystemAppContext(IDataClassHelper database)
        {
            dataBaseClassHelper = database;
        }
        public IDataClassHelper DataBaseClassHelper()
        {
            return dataBaseClassHelper;
        }
    }

    public interface ISystemAppContext
    {

        IDataClassHelper DataBaseClassHelper();
    
    }
}
