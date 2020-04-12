using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace work.api.webapi.Common
{
    /// <summary>
    /// http上下文
    /// </summary>
    public class HttpCurrContext
    {
        public static IHttpContextAccessor Accessor;
        public static HttpContext GetContext()
        {
            return Accessor.HttpContext;
        }
    }

}
