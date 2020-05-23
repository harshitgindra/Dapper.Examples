#region USING
using DapperExamples.Abstraction;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

#endregion
namespace DapperExamples.DAL
{
    public class SqlDatabaseStrategy : IDatabaseStrategy
    {
        public SqlDatabaseStrategy()
        {
        }

        public void SeedDatabase()
        {
            //***
            //*** Add any scripts that will be should be executed for seeding DB
            //***
        }

        public DbConnection Connection
        {
            get
            {
                //***
                //*** Create a new connection if null or disposed
                //***
                if (_connection == null)
                {
                    _connection = new SqlConnection("Data Source=localhost;Initial Catalog=Crayon;Integrated Security=True");
                    _connection.Open();
                }
                //***
                //*** Open the connection if the connection state is anything other than disposed
                //***
                else if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        private DbConnection _connection;
    }
}
