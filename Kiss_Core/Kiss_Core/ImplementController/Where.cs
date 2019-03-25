using Kiss_Core.InterfaceController;
using System;
using System.Collections.Generic;
using System.Data;

namespace Kiss_Core.ImplementController
{
     class Where : IWhere
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IWhere OrderBy(string column, bool asc)
        {
            throw new NotImplementedException();
        }

        public T Select<T>(string field)
        {
            throw new NotImplementedException();
        }

        public DataTable Select(params string[] fields)
        {
            throw new NotImplementedException();
        }

        public List<t> Selects<t>(string field)
        {
            throw new NotImplementedException();
        }

        public IWhere Set(string columnandvalue)
        {
            throw new NotImplementedException();
        }

        public IWhere Set(string column, object value)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        IWhere IWhere.Where(string where, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
