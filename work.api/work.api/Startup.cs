using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using work.Common;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using work.api.Repository;
using work.api.Model;
using Microsoft.AspNetCore.Http;
using work.api.Common;
using work.api.webapi.Common;
using work.api.Common.Redis;

namespace work.api
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
            services.AddControllers();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.1.0",
                    Title = "WebAPI",
                    Description = "WebAPI"

                });


            });
            #endregion

            //模型绑定 特性验证，自定义返回格式
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //获取验证失败的模型字段 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //设置返回内容
                    var result = new ResultModelT<String>
                    {
                        code = System.Net.HttpStatusCode.BadRequest,
                        success = false,
                        body = "",
                        message=str,
                        totalCount=0
                        
                    };
                    return new BadRequestObjectResult(result);
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers().AddControllersAsServices();

            //redis
            var redisConnectionString = ((ConfigurationSection)Configuration.GetSection("RedisConnectionStrings:Connection")).Value;
            var redisInstanceName = ((ConfigurationSection)Configuration.GetSection("RedisConnectionStrings:InstanceName")).Value;
            services.AddSingleton(new RedisCacheHelper(redisConnectionString, redisInstanceName));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            HttpCurrContext.Accessor = accessor;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //如果设置根目录为swagger,将此值置空
                //  c.RoutePrefix = string.Empty;
            });
            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("Service");
            //接口层所在程序集命名空间
            Assembly repository = Assembly.Load("DataAccess");
            //controll
            var controllerBaseType = typeof(ControllerBase);
            //自动注入
            builder.RegisterAssemblyTypes(service, repository)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            //注册仓储，所有IRepository接口到Repository的映射
            builder.RegisterGeneric(typeof(RepositoryBase<>))
                //InstancePerDependency：默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
                .As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }

        
    }
}
