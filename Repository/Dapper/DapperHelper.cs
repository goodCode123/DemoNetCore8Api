using Repository.Enum;

namespace Repository.Dapper
{
    public class DapperHelper
    {
        public static DataBaseFactory NDRS
        {
            get
            {
                return GetDB(DatabaseType.SqlServer, AppSettings.ConnectionStrings.NDRS);
            }
        }


        /// <summary>
        /// GetDB
        /// </summary>
        /// <param name="Type">DB 類型</param>
        /// <param name="Connection">連線字串</param>
        /// <returns></returns>
        public static DataBaseFactory GetDB(DatabaseType Type, string Connection)
        {
            DataBaseFactory dataBaseFactory = new DataBaseFactory(Type, Connection);

            return dataBaseFactory;
        }
    }
}
