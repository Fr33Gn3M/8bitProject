using FC.FileBusiness.Impl;
using FC.FileBusiness.Services;
using FC.Utils.AppSetting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//×¢²áÅäÖÃÎÄ¼þ
builder.Services.AddSingleton(new AppSettingHelper());
//×¢²áServiceµ¥Àý
builder.Services.AddSingleton<IFileService, FileService>();

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
