using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using work.api.Common.Json;
using work.api.Model;

namespace work.api.webapi.Common
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =false)]
    public class AuthAttribute : ActionFilterAttribute
    {
        /// <summary> 
        /// 权限代码 
        /// </summary> 
        public AuthCodeEnum Code { get; set; }

        /// <summary> 
        /// 验证权限
        /// </summary> 
        /// <param name="filterContext"></param> 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //如果存在身份信息 
            if (Current.CurrentUser != null)
            {
                if (Code == AuthCodeEnum.Public)
                    return;
            }
            else
            {
                if (Code == AuthCodeEnum.Public) { return; }

                var result = new ResultModelT<String>
                {
                    body = "",
                    success = false,
                    code = System.Net.HttpStatusCode.Unauthorized,
                    message = "请重新登陆",

                };
                context.Result = new BadRequestObjectResult(JsonHelper.SerializeJSON(result));

            }
        }
    }

    public enum AuthCodeEnum
    {
        Public = 1,
        Login = 2
    }

}