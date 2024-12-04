using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.SQL
{
    public interface ISQLiteService
    {
        private string GetPath()
        {
            string dbName = "inntecMovil.db3";
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), dbName);
            return path;
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }

        public SQLiteAsyncConnection GetConnectionAsync()
        {
            return new SQLiteAsyncConnection(GetPath());
        }
    }
}
