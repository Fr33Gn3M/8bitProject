using FC.Core.Models;
using FC.FileBusiness.Services;
using FC.Utils.AppSetting;
using Microsoft.AspNetCore.Mvc;

namespace FC.BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShanShanController : ControllerBase
    {
        private readonly IFileService fileService;

        public ShanShanController(IFileService _fileService)
        {
            fileService = _fileService;
        }

        [HttpGet("test")]
        public ApiResult Test()
        {
            return ApiResult.Ok();
        }

        [HttpPost("lawnExcelToWord")]
        public ApiResult LawnExcelToWord(IFormFile file)
        {
            if (file.Length <= 0)
                return ApiResult.Error(ApiResultCode.PARAM_ERROR, "上传的文件有误，请重新上传!");

            using Stream stream = file.OpenReadStream();
            string fileName = fileService.LawnExcelToWord(stream);
            return ApiResult.Ok(fileName);
        }

        [HttpGet("downloadWord")]
        public FileStreamResult DownloadWord(string docName)
        {
            string path = Path.Combine(AppSettingHelper.ReadString("FilePath", "ExportDir"), docName);
            return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read), "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
    }
}
