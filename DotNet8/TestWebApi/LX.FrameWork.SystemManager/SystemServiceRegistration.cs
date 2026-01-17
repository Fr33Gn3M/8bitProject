using LX.FrameWork.DataModels;
using LX.FrameWork.SystemManager.Impls;
using LX.FrameWork.SystemManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sys.DataBase;

namespace LX.FrameWork.SystemManager
{
    public static class SystemServiceRegistration
    {
        public static IServiceCollection AddSystemServices(this IServiceCollection services, string connectionString)
        {

            var dataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, connectionString, typeof(DBBase).Namespace);
            var systemAppContext = new SystemAppContext(dataBaseClassHelper);
            services.AddSingleton<ISystemAppContext>(systemAppContext);
            services.AddSingleton<IDataServiceRepo, DataServiceRepo>();

            return services;

        }
    }
}
