using Chloe.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace work.api.Repository
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private string _dbName = null;
        private bool _isRead;
        public SqlConnectionFactory(string dbName, bool isRead = true)
        {
            _dbName = dbName;
            _isRead = isRead;
        }
        public IDbConnection CreateConnection()
        {
           // var connStr = ((ConfigurationSection)Configuration.GetSection("ConnectionString")).Value;
            return new MySqlConnection("server=121.43.233.78;port=3306;database=work;User Name=root;Password=Work@123456;");
        }
    }
}
