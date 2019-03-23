using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Kiss_Core
{
    public class BaseDBContext
    {
        private string connectionString;
        public IDbConnection Connection { get; set; }

        protected BaseDBContext(DataBaseConfig settings)
        {
            connectionString = settings.ConnectionString;
            Connection = new MySqlConnection(connectionString);
        }
    }
}
