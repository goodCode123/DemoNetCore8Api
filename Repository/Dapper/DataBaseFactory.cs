using Dapper;
using Microsoft.Data.SqlClient;
using Repository.Enum;
using System.Data;
using static Dapper.SimpleCRUD;
using static Dapper.SqlMapper;

namespace Repository.Dapper
{
    public class DataBaseFactory
    {
        private static int commandTimeout = 30;

        private IDbConnection _sharedConnection;

        public DataBaseFactory(DatabaseType dbType, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException(string.Format("DB Name={0}，查無連線字串!", connectionString));
            }

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    this._sharedConnection = new SqlConnection(connectionString);
                    break;
                    //case DatabaseType.MySql:
                    //    this._sharedConnection = new MySqlConnection(connectionString);
                    //    break;
            }
        }

        /// <summary>
        /// 新增單筆資料
        /// </summary>
        /// <typeparam name="T">資料實體類型</typeparam>
        /// <param name="entity">實體對象</param>
        /// <param name="transaction">資料庫交易</param>
        /// <returns>受影響的資料筆數</returns>
        public long Insert<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            return (int)(_sharedConnection.Insert(entity, transaction, commandTimeout) ?? 0);
        }

        /// <summary>
        /// 新增多筆資料
        /// </summary>
        /// <typeparam name="T">資料實體類型</typeparam>
        /// <param name="entities">實體對象列表</param>
        /// <param name="transaction">資料庫交易</param>
        /// <returns>受影響的資料筆數</returns>
        public long Insert<T>(List<T> entitys, IDbTransaction transaction = null) where T : class
        {
            var reslut = entitys.Sum(entity => (int)(_sharedConnection.Insert(entity, transaction, commandTimeout) ?? 0));

            return reslut;
        }

        /// <summary>
        /// 更新單筆資料
        /// </summary>
        /// <typeparam name="T">資料實體類型</typeparam>
        /// <param name="entity">實體對象</param>
        /// <param name="transaction">資料庫交易</param>
        /// <returns>受影響的資料筆數</returns>
        public int Update<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            return (int)(_sharedConnection.Update(entity, transaction, commandTimeout));
        }

        /// <summary>
        /// 更新多筆資料
        /// </summary>
        /// <typeparam name="T">資料實體類型</typeparam>
        /// <param name="entities">實體對象列表</param>
        /// <param name="transaction">資料庫交易</param>
        /// <returns>受影響的資料筆數</returns>
        public int Update<T>(List<T> entitys, IDbTransaction transaction = null) where T : class
        {
            int reslut = entitys.Sum(entity => (int)(_sharedConnection.Update(entity, transaction, commandTimeout)));
            return reslut;
        }


        /// <summary>
        /// 刪除單筆資料
        /// </summary>
        /// <typeparam name="T">資料實體類型</typeparam>
        /// <param name="entity">實體對象</param>
        /// <param name="transaction">資料庫交易</param>
        /// <returns>受影響的資料筆數</returns>
        public int Delete<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            // 使用 Dapper.SimpleCRUD 的 Delete 方法
            return _sharedConnection.Delete(entity, transaction, commandTimeout);
        }

        /// <summary>
        /// 刪除多筆資料
        /// </summary>
        /// <typeparam name="T">資料實體類型</typeparam>
        /// <param name="entities">實體對象列表</param>
        /// <param name="transaction">資料庫交易</param>
        /// <returns>受影響的資料筆數</returns>
        public int Delete<T>(List<T> entitys, IDbTransaction transaction = null) where T : class
        {
            int reslut = entitys.Sum(entity => _sharedConnection.Delete(entity, transaction, commandTimeout));

            return reslut;
        }

        /// <summary>
        /// Dapper.SimpleCRUD 查詢單個資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById<T>(object id) where T : class
        {
            return _sharedConnection.Get<T>(id);
        }

        /// <summary>
        ///  Dapper.SimpleCRUD 查詢所有資料，或者帶有條件的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>(object condition = null) where T : class
        {
            // 使用 Dapper.SimpleCRUD 的 GetList 方法

            return _sharedConnection.GetList<T>(condition);
        }

        /// <summary>
        /// Sql批量更新、刪除、新增
        /// </summary>
        public int BatchExecute(string sql, object parameters)
        {
            using (IDbConnection conn = _sharedConnection)
            {
                conn.Open();

                return conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// Sql查詢
        /// </summary>
        public List<T> GetList<T>(string sql, object parameters = null)
        {
            using (IDbConnection conn = _sharedConnection)
            {
                conn.Open();

                return conn.Query<T>(sql, parameters).ToList();
            }
        }
    }
}
