using FC.Core.AppSetting;
using FC.Database;
using FC.Database.DataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//注册配置文件
builder.Services.AddSingleton(new AppSettingHelper());
//注册Service单例
builder.Services.AddSingleton<IDataService, DataService>();
//添加动态数据库支持
builder.Services.AddTransient<AppDb>(_ => new AppDb(builder.Configuration["DbType"], builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
