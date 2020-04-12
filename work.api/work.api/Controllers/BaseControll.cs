using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work.api.Model;
using work.api.webapi.Common;

namespace work.api.Controllers
{
    [Auth(Code = AuthCodeEnum.Login)]
    public class BaseControll : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            var result = new ResultModelT<object>();
            result.Success("请求成功", value);
            return base.Ok(result);
        }


    }
}
