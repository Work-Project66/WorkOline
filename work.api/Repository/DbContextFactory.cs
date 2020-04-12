using Chloe;
using Chloe.MySql;

namespace work.api.Repository
{
    public class DbContextFactory
    {
        private DataBaseTypeEnum DbType;
        private DB DbName;
        private bool IsRead;
        private bool IsDBMS;
        public DbContextFactory(DataBaseTypeEnum dbType, DB dbName, bool isRead = true, bool isDBMS = false)
        {
            DbType = dbType;
            DbName = dbName;
            IsRead = isRead;
            IsDBMS = isDBMS;
        }
        public IDbContext Instance
        {
            get
            {
                IDbContext context;

                context = new MySqlContext(new SqlConnectionFactory(DbName.ToString(), IsRead));

                return context;
            }
        }
    }
}
