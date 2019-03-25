﻿using System.Collections.Generic;

namespace Kiss_Core
{
     interface IBaseRepository<T>
    {
        int Insert(string insertSql);

        int Update(string updateSql);

        int Delete(string deleteSql);

        object Select(string selectOneSql);

        IList<T> Selects(string selectSql);
    }
}
