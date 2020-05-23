using Dapper;
using DapperExamples.Abstraction;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace DapperExamples.DAL
{
    public class SqliteDatabaseContext: IDatabaseContext
    {
        string DbFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserDb.sqlite");

        public SqliteDatabaseContext()
        {
        }

        public void SeedDatabase()
        {
            if (!File.Exists(DbFile))
            {
                SQLiteConnection.CreateFile(DbFile);
                this.Connection.Execute(@"
        CREATE TABLE IF NOT EXISTS [AppUser]  (
                 Id                                 integer primary key AUTOINCREMENT,
                 FirstName                           nvarchar(100) not null,
                 LastName                            nvarchar(100) not null,
                 Age                         int not null
              )");
            }
        }

        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection("Data Source=" + DbFile);
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
