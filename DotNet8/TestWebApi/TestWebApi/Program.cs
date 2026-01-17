using LX.Commons.Common;
using LX.FrameWork.DataModels;
using LX.FrameWork.SystemManager;
using LX.FrameWork.SystemManager.Impls;
using LX.FrameWork.SystemManager.Interfaces;
using LX.WebCommons.Filters;
using Sys.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mainProjectConfiguration = builder.Configuration;

// 注入主项目的 IConfiguration 到服务容器
builder.Services.AddSingleton<IConfiguration>(mainProjectConfiguration);

// 调用扩展方法注册服务，并传入 IConfiguration 实例
builder.Services.AddSystemServices(mainProjectConfiguration["ConnectionStrings:ParkingConntionString"]);

// 注册类库中的配置辅助类
builder.Services.AddTransient<LX.Commons.Common.ConfigurationManagerHelper>();
builder.Services.AddTransient<Sys.DataBase.Common.ConfigurationManagerHelper>();

builder.Services.AddControllers(options =>
{
    // 注册全局异常过滤器
    options.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// 初始化 LogHelper 的 ConfigManager
using (var scope = app.Services.CreateScope())
{
    var configManager = scope.ServiceProvider.GetRequiredService<LX.Commons.Common.ConfigurationManagerHelper>();
    ScopeManager.ConfigManager = configManager;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
