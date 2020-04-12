using System;
using System.Collections.Generic;
using System.Text;
using work.api.Entity.TableEntity;

namespace work.api.Service.IService
{
    public interface IUserService
    {
        userEntity GetUser(long id);
    }
}
