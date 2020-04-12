using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using work.api.Entity.TableEntity;
using work.api.Model;
using work.api.Service.IService;
using work.api.webapi.Common;

namespace work.api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserControll: BaseControll
    {
        public IUserService IUserService { get; set; }
        [HttpGet]
        public ObjectResult getUserInfo([Required(ErrorMessage ="用户ID不能为空")]long id)
        {
            var result = IUserService.GetUser(id);
            return Ok(result);

        }

    }
}
