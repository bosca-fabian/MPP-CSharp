using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionDBLibrary;
using Npgsql;

namespace MPPCSharp.Utils
{
    public class JdbcUtils
    {
        private static NpgsqlConnection instance = null;

        public static NpgsqlConnection GetConnection(IDictionary<string, string> parameters)
        {
            if (instance == null || instance.State == ConnectionState.Closed)
            {
                instance = GetNewConnection(parameters);
                instance.Open();
            }
            return instance;
        }

        private static NpgsqlConnection GetNewConnection(IDictionary<string, string> parameters)
        {
            return ConnectionFactory.getInstance().createConnection(parameters);
        }
    }
}
