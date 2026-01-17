using Microsoft.Extensions.DependencyInjection;
using FC.Database.Dao;
using FC.Database.EnumModels;

namespace FC.Database
{
    public static class DatabaseServicesRegistration
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string serviceKey, string sqlTypeName, string connectionString)
        {
            // 使用枚举解析并提供默认值，避免空值
            if (!Enum.TryParse<DatabaseType>(sqlTypeName, true, out var sqlType))
            {
                // 记录错误日志（如果有日志服务）
                var invalidTypes = string.Join(", ", Enum.GetNames(typeof(DatabaseType)));
                throw new ArgumentException(
                    $"无效的数据库类型: '{sqlTypeName}'. 有效类型为: {invalidTypes}",
                    nameof(sqlTypeName)
                );
            }

            // 使用 AddKeyedSingleton 注册单例服务
            services.AddKeyedSingleton<IDb>(serviceKey, (sp, key) =>
                new Db(sqlType, connectionString)
            );

            return services;
        }
    }
}
