using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons;
using ExceptionManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestOne.Controllers
{
    /// <summary>
    /// 文件系统控制器
    /// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class FileController : ControllerBase
	{
        [HttpPost("test")]
        public JsonResult test()
        {
            var result = new JsonResultModel() { };
            throw new ApiException(ResultCode.FAIL);

            JsonResult re = new JsonResult(result);
            return re;
        }
    }
}
