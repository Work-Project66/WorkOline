using System;
using work.api.Entity.TableEntity;
using work.api.Repository;
using work.Common;

namespace work.api.DataAccess
{
    public class UserDataAccess : RepositoryBase<userEntity>, IDependency
    {
        public UserDataAccess() : base(DataBaseTypeEnum.MySql, DB.Work)
        {
            
        }
    }
}
