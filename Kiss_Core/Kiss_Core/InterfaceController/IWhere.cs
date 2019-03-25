using System.Collections.Generic;
using System.Data;

namespace Kiss_Core.InterfaceController
{
    interface IWhere
    {
        int Count();

        IWhere Set(string columnandvalue);

        IWhere Set(string column, object value);

        IWhere Where(string where, params object[] args);

        IWhere OrderBy(string column, bool asc);
    }
}
