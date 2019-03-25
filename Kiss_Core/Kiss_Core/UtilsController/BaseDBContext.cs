using MySql.Data.MySqlClient;
using System.Data;

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
