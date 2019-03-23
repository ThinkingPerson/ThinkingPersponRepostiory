
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Kiss_Core
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        //public BaseDBContext _baseDB;
        public IDbCommand command;
        public BaseRepository(BaseDBContext baseDB)
        {
            IDbConnection dbConnection = baseDB.Connection;
            dbConnection.Open();

            command = dbConnection.CreateCommand();

        }

        public int Delete(int Id, string deleteSql)
        {


            return command.e
            //return command.ExecuteAsync(deleteSql, new { Id = Id });
        }

        public T Select(int Id, string selectOneSql)
        {
            using (IDbConnection dbConnection = _baseDB.Connection)
            {
                dbConnection.Open();
                var result = dbConnection.QueryFirstOrDefaultAsync<T>(selectOneSql, new { Id = Id });
                return result;
            }
        }
        public async List<T> GetList(int Id, string selectSql)
        {
            using (IDbConnection dbConnection = _baseDB.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(selectSql, new { Id = Id }).ToList();
            }
        }
        public int Insert(T entity, string insertSql)
        {


            command.CommandText = "INSERT INTO " + entity + " () VALUES ('" + userName + "')";

            return dbConnection.QueryFirstOrDefaultAsync<T>(insertSql, entity);
        }

        public int Update(T entity, string updateSql)
        {
            using (IDbConnection dbConnection = _baseDB.Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync(updateSql, entity);
            }
        }
    }
}
