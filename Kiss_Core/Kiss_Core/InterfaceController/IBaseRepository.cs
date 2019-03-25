using System.Collections.Generic;
using System.Data;

namespace Kiss_Core
{
    public interface IBaseRepository<T>
    {
        int Insert(string insertSql);

        int Update(string updateSql);

        int Delete(string deleteSql);

        object Select(string selectOneSql);

        IList<T> Selects(string selectSql);
    }
}
