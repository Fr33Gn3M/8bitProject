using FC.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FC.BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShanShanController : ControllerBase
    {
        [HttpPost("lawnExcelToWord")]
        public ApiResult LawnExcelToWord()
        {
            var files = Request.Form.Files;
            if (files.Count == 0) return ApiResult.Error(ApiResultCode.PARAM_ERROR, "请上传Excel");


            return ApiResult.Ok();
        }


    }
}
