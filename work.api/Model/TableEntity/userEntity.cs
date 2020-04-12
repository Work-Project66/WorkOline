

using System;
using Chloe.Annotations;
namespace work.api.Entity.TableEntity
{
    /// <summary>
    ///userEntity
    /// </summary>
    [TableAttribute("user")]
    public partial class userEntity 
    {
        public userEntity()
        {
            USER_NAME = string.Empty;
            MOBILE = string.Empty;
            NICKNAME = string.Empty;
            IDENTITY_ID = string.Empty;
            OPEN_ID = string.Empty;
            CREATE_DATE =  new DateTime(1970,1,1);
            UPDATE_DATE =  new DateTime(1970,1,1);
            REMARK = string.Empty;
        }
        /// <summary>
        ///主键
        /// </summary>
        [ColumnAttribute("ID",IsPrimaryKey = true)]
        public long ID { get; set; }
        /// <summary>
        ///用户名
        /// </summary>
        [ColumnAttribute("USER_NAME")]  
        public string USER_NAME { get; set; }
        /// <summary>
        ///手机号
        /// </summary>
        [ColumnAttribute("MOBILE")]  
        public string MOBILE { get; set; }
        /// <summary>
        ///年龄
        /// </summary>
        [ColumnAttribute("AGE")]  
        public int AGE { get; set; }
        /// <summary>
        ///性别
        /// </summary>
        [ColumnAttribute("GENDER")]  
        public int GENDER { get; set; }
        /// <summary>
        ///昵称
        /// </summary>
        [ColumnAttribute("NICKNAME")]  
        public string NICKNAME { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        [ColumnAttribute("IDENTITY_ID")]  
        public string IDENTITY_ID { get; set; }
        /// <summary>
        ///微信ID
        /// </summary>
        [ColumnAttribute("OPEN_ID")]  
        public string OPEN_ID { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        [ColumnAttribute("CREATE_DATE")]  
        public DateTime CREATE_DATE { get; set; }
        /// <summary>
        ///更新时间
        /// </summary>
        [ColumnAttribute("UPDATE_DATE")]  
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        [ColumnAttribute("REMARK")]  
        public string REMARK { get; set; }
    }
}