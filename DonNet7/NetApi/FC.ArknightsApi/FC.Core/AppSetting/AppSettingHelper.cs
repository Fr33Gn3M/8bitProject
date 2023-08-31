using Microsoft.Extensions.Configuration;

namespace FC.Core.AppSetting
{
    public class AppSettingHelper
    {
        private static IConfiguration? _config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置文件</param>
        public AppSettingHelper()
        {
            _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, reloadOnChange: true)
                    .Build();
        }

        /// <summary>
        /// 读取指定节点信息
        /// </summary>
        /// <param name="sessions">节点名称</param>
        /// <returns></returns>
        public static string ReadString(params string[] sessions)
        {
            try
            {
                if (_config != null && sessions.Any())
                {
                    string? str = _config[string.Join(":", sessions)];
                    if (!string.IsNullOrEmpty(str))
                    {
                        return str;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// 读取实体信息
        /// </summary>
        /// <param name="sessions">节点名称</param>
        /// <returns></returns>
        public static T ReadObject<T>(params string[] sessions) where T : class, new()
        {
            T data = new();
            try
            {
                if (_config != null && sessions.Any())
                {
                    _config.Bind(string.Join(":", sessions), data);
                }
            }
            catch
            {
                return data;
            }
            return data;
        }

        /// <summary>
        /// 读取实体数组信息
        /// </summary>
        /// <param name="sessions">节点名称</param>
        /// <returns></returns>
        public static List<T> ReadList<T>(params string[] sessions) where T : class
        {
            List<T> list = new();
            try
            {
                if (_config != null && sessions.Any())
                {
                    _config.Bind(string.Join(":", sessions), list);
                }
            }
            catch
            {
                return list;
            }
            return list;
        }

    }
}
