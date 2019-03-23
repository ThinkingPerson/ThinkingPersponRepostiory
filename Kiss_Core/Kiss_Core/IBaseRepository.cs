using System.Collections.Generic;
using System.Data;

namespace Kiss_Core
{
    public interface IBaseRepository<T>
    {
        int Insert(T entity, string insertSql);

        int Update(T entity, string updateSql);

        int Delete(int Id, string deleteSql);

        T Select(int Id, string selectOneSql);

        //DataTable Select(params string[] fields);

        List<T> GetList(int Id, string selectSql);
    }
}
