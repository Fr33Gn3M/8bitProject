using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LX.Commons.Common
{
    public class ConfigurationManagerHelper
    {
        private readonly IConfiguration _mainProjectConfig;

        public ConfigurationManagerHelper(IConfiguration mainProjectConfig)
        {
            _mainProjectConfig = mainProjectConfig;
        }

        /// <summary>
        /// 获取主项目配置
        /// </summary>
        /// <param name="key">格式 根据深度调整 xxx:yyy:zzz</param>
        /// <returns></returns>
        public string GetMainProjectSetting(string key)
        {
            return _mainProjectConfig[key];
        }

        /// <summary>
        /// 获取主项目配置
        /// </summary>
        /// <typeparam name="T">对象模型</typeparam>
        /// <param name="key">格式 根据深度调整 xxx:yyy:zzz</param>
        /// <returns></returns>
        public T GetMainProjectSetting<T>(string key)
        {
            string strValue = _mainProjectConfig[key];
            return JsonConvert.DeserializeObject<T>(strValue);
        }

    }
}
