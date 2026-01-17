using System.Collections.Generic;
using LX.WebCommons.Models;
using Sys.DataBase;

namespace LX.FrameWork.SystemManager.Interfaces
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public interface IDataServiceRepo
    {

        JsonResults QueryNoToken(QueryPageFilter filter);

    }
}
