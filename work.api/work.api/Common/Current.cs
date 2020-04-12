using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using work.api.Common.Redis;
using work.api.Model.Common;


namespace work.api.webapi.Common
{
    public class Current
    {
        public static CurrentUserModel CurrentUser
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpCurrContext.Accessor.HttpContext.Request.Headers["ThirdSessionId"]))
                {
                    return RedisCacheHelper.Get<CurrentUserModel>(HttpCurrContext.Accessor.HttpContext.Request.Headers["ThirdSessionId"]);
                }
                return null;
            }
        }
    }
}