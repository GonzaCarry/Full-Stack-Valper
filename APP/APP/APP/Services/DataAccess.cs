using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.IO;
using SQLite;
using APP.Models;

namespace APP.Services
{
    class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(Path.Combine(config.DirDB, "ValperDB.db3"), false);
            connection.CreateTable<Function>();
        }

        public void InsertFunction(Function function)
        {
            connection.Insert(function);
        }

        public void ModifyFunction(Function function)
        {
            connection.Update(function);
        }

        public void DeleteFunction(Function function)
        {
            connection.Delete(function);
        }

        public Function GetFunction(int IDFunction)
        {
            return connection.Table<Function>().FirstOrDefault(c => c.Id.Equals(IDFunction));
        }

        public List<Function> GetFunctions()
        {
            return connection.Table<Function>().OrderBy(c => c.Server).ToList();
        }

        public Connection GetConnection()
        {
            if (connection.Table<Connection>().ToList().Count > 0)
            {
                return connection.Table<Connection>().FirstOrDefault(c => c.Id.Equals(0));
            }
            return new Connection();
        }

        public void InsertConnection(Connection con)
        {
            if (connection.Table<Connection>().ToList().Count > 0)
            {
                connection.Delete(connection.Table<Connection>().FirstOrDefault(c => c.Id.Equals(0)));
            }
            connection.Insert(con);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
