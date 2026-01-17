
using FC.Core.Models;
using FC.Database.Dao;
using Microsoft.AspNetCore.Mvc;

namespace FC.BaseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {

        private readonly IDb _mainDb;
        private readonly IDb _historyDb;

        public DataController(IServiceProvider serviceProvider)
        {
            _mainDb = serviceProvider.GetRequiredKeyedService<IDb>("Main");
            _historyDb = serviceProvider.GetRequiredKeyedService<IDb>("History");
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{resource}/{id}")]
        public ApiResult Get(string resource, int id)
        {
            var result = _mainDb.GetDataService().Get(resource, id);
            return ApiResult.Ok(result);
        }
    }
}
