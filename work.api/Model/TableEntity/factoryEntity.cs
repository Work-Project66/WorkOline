

using System;
using Chloe.Annotations;
namespace work.api.Entity.TableEntity
{
    /// <summary>
    ///factoryEntity
    /// </summary>
    [TableAttribute("factory")]
    public partial class factoryEntity 
    {
        public factoryEntity()
        {
            FACTORY_NAME = string.Empty;
            FACTORY_ADRESS = string.Empty;
            FACTORY_TEL = string.Empty;
            FACTORY_INFO = string.Empty;
            CREATE_DATE =  new DateTime(1970,1,1);
        }
        /// <summary>
        ///工厂主键
        /// </summary>
        [ColumnAttribute("ID",IsPrimaryKey = true)]
        public long ID { get; set; }
        /// <summary>
        ///工厂名称
        /// </summary>
        [ColumnAttribute("FACTORY_NAME")]  
        public string FACTORY_NAME { get; set; }
        /// <summary>
        ///工厂地址
        /// </summary>
        [ColumnAttribute("FACTORY_ADRESS")]  
        public string FACTORY_ADRESS { get; set; }
        /// <summary>
        ///工厂电话
        /// </summary>
        [ColumnAttribute("FACTORY_TEL")]  
        public string FACTORY_TEL { get; set; }
        /// <summary>
        ///工厂信息
        /// </summary>
        [ColumnAttribute("FACTORY_INFO")]  
        public string FACTORY_INFO { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        [ColumnAttribute("CREATE_DATE")]  
        public DateTime CREATE_DATE { get; set; }
        /// <summary>
        ///创建人
        /// </summary>
        [ColumnAttribute("CREATE_ID")]  
        public long CREATE_ID { get; set; }
    }
}