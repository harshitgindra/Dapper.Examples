#region USING
using Dapper;
using DapperExamples.Abstraction;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
#endregion

namespace DapperExamples.DAL
{
    public class SqliteDatabaseStrategy : IDatabaseStrategy
    {
        string SqliteFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserDb.sqlite");

        public SqliteDatabaseStrategy()
        {
        }

        public void SeedDatabase()
        {
            //***
            //*** Check if file exist 
            //***
            if (!File.Exists(SqliteFilePath))
            {
                //***
                //***  Sqlite file does not exist, create a new file
                //***
                SQLiteConnection.CreateFile(SqliteFilePath);
                //***
                //*** Execute script to create a new table 
                //***
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
                //***
                //*** Create a new connection if null or disposed
                //***
                if (_connection == null)
                {
                    _connection = new SQLiteConnection("Data Source=" + SqliteFilePath);
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
