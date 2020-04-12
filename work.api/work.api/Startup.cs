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

            //ģ�Ͱ� ������֤���Զ��巵�ظ�ʽ
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //��ȡ��֤ʧ�ܵ�ģ���ֶ� 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //���÷�������
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
                //������ø�Ŀ¼Ϊswagger,����ֵ�ÿ�
                //  c.RoutePrefix = string.Empty;
            });
            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ҵ���߼������ڳ��������ռ�
            Assembly service = Assembly.Load("Service");
            //�ӿڲ����ڳ��������ռ�
            Assembly repository = Assembly.Load("DataAccess");
            //controll
            var controllerBaseType = typeof(ControllerBase);
            //�Զ�ע��
            builder.RegisterAssemblyTypes(service, repository)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            //ע��ִ�������IRepository�ӿڵ�Repository��ӳ��
            builder.RegisterGeneric(typeof(RepositoryBase<>))
                //InstancePerDependency��Ĭ��ģʽ��ÿ�ε��ã���������ʵ��������ÿ�����󶼴���һ���µĶ���
                .As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }

        
    }
}
