using System;
using System.Collections.Generic;
using System.Text;
using work.api.DataAccess;
using work.api.Entity.TableEntity;
using work.api.Service.IService;
using work.Common;

namespace work.api.Service
{
    public class UserService: IUserService, IDependency
    {
        public UserDataAccess userDataAccess = new UserDataAccess();
        public userEntity GetUser(long id)
        {
            return userDataAccess.FindByPrimaryKey(id);
        }
    }
}
