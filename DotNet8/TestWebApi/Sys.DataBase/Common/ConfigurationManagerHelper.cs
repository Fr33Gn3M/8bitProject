using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Sys.DataBase.Common
{
    public class ConfigurationManagerHelper
    {
        private readonly IConfiguration _mainProjectConfig;
        private readonly IConfiguration _classLibraryConfig;

        public ConfigurationManagerHelper(IConfiguration mainProjectConfig)
        {
            _mainProjectConfig = mainProjectConfig;

            // 加载类库自己的 appsettings.json
            var classLibraryConfigBuilder = new ConfigurationBuilder()
               .SetBasePath(Path.GetDirectoryName(typeof(ConfigurationManagerHelper).Assembly.Location))
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _classLibraryConfig = classLibraryConfigBuilder.Build();
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
        /// 获取本项目配置，key格式 xxx:yyy:zzz
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetClassLibrarySetting(string key)
        {
            return _classLibraryConfig[key];
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

        /// <summary>
        /// 获取本项目配置，key格式 xxx:yyy:zzz
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetClassLibrarySetting<T>(string key)
        {
            string strValue = _classLibraryConfig[key];
            return JsonConvert.DeserializeObject<T>(strValue);
        }

    }
}
