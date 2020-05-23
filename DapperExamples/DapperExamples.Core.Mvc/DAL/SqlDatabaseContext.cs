using Dapper;
using DapperExamples.Abstraction;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace DapperExamples.DAL
{
    public class SqlDatabaseContext : IDatabaseContext
    {
        public SqlDatabaseContext()
        {
        }

        public void SeedDatabase()
        {

        }

        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection("Data Source=localhost;Initial Catalog=Crayon;Integrated Security=True");
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        private DbConnection _connection;
    }
}
