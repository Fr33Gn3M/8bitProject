using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TestOne.Commons;
using TestOne.Filters;

namespace TestOne
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var title = "myApi";
            var version = "v1";
            services.AddControllers(options=>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
            services.AddSwaggerGen(c =>
            {
                // swagger文档配置
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = title,
                    //Description = $"{title} HTTP API " + v,
                    //Contact = new OpenApiContact { Name = "Contact", Email = "xx@xxx.xx", Url = new Uri("https://www.cnblogs.com/straycats/") },
                    //License = new OpenApiLicense { Name = "License", Url = new Uri("https://www.cnblogs.com/straycats/") }
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Core.Admin.webapi.xml是我的项目生成XML文档的后缀名,具体的以你项目为主
                var xmlPath = Path.Combine(basePath, "TestOne.xml");
                c.IncludeXmlComments(xmlPath);

            });
            services.AddMvc().AddJsonOptions((options) => {
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
