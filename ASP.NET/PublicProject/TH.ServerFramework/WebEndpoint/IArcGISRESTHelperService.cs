/************************************************************************************ 
 * * Copyright (c) 2014  All Rights Reserved. 
 * * CLR版本： 4.0.30319.34011 
 * * 机器名称：192.168.202.67 
 * * 版本号：  V1.0.0.0 
 * * 当前的用户域：192.168.202.67 
 * * 创建人：  huzm 
 * * 创建时间：2014/4/4 14:56:16 
 * * 描述： 
 * *  
 * *  
 * * ===================================================================== 
 * * 修改标记  
 * * 修改时间：2014/4/4 14:56:16 
 * * 修改人： huzm 
 * * 版本号： V1.0.0.0 
 * * 描述： 
 * *  
 * *  
 * *  
 * ************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace TH.ServerFramework.WebEndpoint
{
    [ServiceContract]
    public interface IArcGISRESTHelperService
    {
        [OperationContract]
        Dictionary<string, string> GetMapServiceUrl(Guid token, string layerName);
    }
}
