using LX.FrameWork.SystemManager.Impls;
using LX.FrameWork.SystemManager.Interfaces;
using LX.WebCommons.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sys.DataBase;

namespace TestWebApi.Controllers.Base
{
    [Route("[controller]")]
    [ApiController]
    public class DataServiceController : ControllerBase
    {

        private readonly IDataServiceRepo DataServiceRepo;

        public DataServiceController(IDataServiceRepo _dataServiceRepo) 
        {
            DataServiceRepo = _dataServiceRepo;
        }

        [HttpPost]
        public ActionResult<JsonResults> QueryNoToken([FromForm]string filter)
        {
            var filterObj = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
            var result = DataServiceRepo.QueryNoToken(filterObj);
            return JsonResults.Success(result);
        }
    }
}
