using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH.ExceptionManager;

namespace FD.ModelCreator.WebSerice.Controllers
{
    public class BussinessServiceController : Controller
    {
        public JsonResult Query()
        {
            var result = new DataBase.JsonResult() { };
            try
            {
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Error = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

    }
}