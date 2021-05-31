using XFApp1.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFApp1.ViewModels;

namespace XFApp1.ViewModels
{
    public class SQLiteViewModel : BaseViewModel
    {
        string dbPath => System.IO.Path.Combine(FileSystem.AppDataDirectory, "Db1.db");
        string dbName => Path.GetFileName(dbPath);

        string dbConnection = "Data Source = {0}";
        public Command DbInfoCommand { get; }
        public Command DbCreateCommand { get; }
        public Command DbDeleteCommand { get; }
        public Command DbDropTableCommand { get; }
        public Command DbAddRowCommand { get; }
        public Command DbGetDataCommand { get; }


        private string info;
        private string dbInfo;
        public string Info { get => info; set => SetProperty(ref info, value); }
        public string DbInfo { get => dbInfo; set => SetProperty(ref dbInfo, value); }

        public SQLiteViewModel()
        {
            try
            {
                Title = "SQLite Tests";
                DbInfoCommand = new Command(() => DbInfo = GetDbInfo(dbPath));
                DbCreateCommand = new Command(DbCreate);
                DbDeleteCommand = new Command(DbDelete);
                DbDropTableCommand = new Command(DbDropTable);
                DbAddRowCommand = new Command(DbAddRow);
                DbGetDataCommand = new Command(DbGetData);

                DbInfo = GetDbInfo(dbPath);
            }
            catch (Exception ex)
            {
                Info = $"{MethodBase.GetCurrentMethod().Name}: {ex.Message}";
            }
        }

        private void DbCreate()
        {
            try
            {
                DbInfo = GetDbInfo(dbPath);

                SqliteConnection conn = new SqliteConnection(string.Format(dbConnection, dbPath));

                string sql = $@"CREATE TABLE [People]( [ID] NVARCHAR PRIMARY KEY, [NAme] NVARCHAR) WITHOUT ROWID;";
                using (var c = new SqliteConnection(string.Format(dbConnection, dbPath)))
                {
                    c.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, c);
                    cmd.ExecuteNonQuery();
                }
                Info = "Db created at: " + dbPath;
                DbInfo = GetDbInfo(dbPath);
            }
            catch (Exception ex)
            {
                Info = $"{MethodBase.GetCurrentMethod().Name}: {ex.Message}";
            }
        }

        private void DbDelete()
        {
            try
            {
                File.Delete(dbPath);
                Info = $"File '{dbName}' deleted";
                DbInfo = GetDbInfo(dbPath);
            }
            catch (Exception ex)
            {
                Info = $"{MethodBase.GetCurrentMethod().Name}: {ex.Message}";
            }
        }

        private void DbDropTable()
        {
            string tableName = "People";
            try
            {
                string sql = $"DROP Table '{tableName}'";
                using (var c = new SqliteConnection(string.Format(dbConnection, dbPath)))
                {
                    c.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, c);
                    cmd.ExecuteNonQuery();
                }
                Info = $"Table '{dbName}' deleted";
                DbInfo = GetDbInfo(dbPath);
            }
            catch (Exception ex)
            {
                Info = $"{MethodBase.GetCurrentMethod().Name}: {ex.Message}";
            }
        }

        private void DbAddRow()
        {
            try
            {
                string sql = $"INSERT INTO People (ID, Name) VALUES('{Guid.NewGuid()}', 'Fred{DateTime.Now.Millisecond}'); ";
                using (var c = new SqliteConnection(string.Format(dbConnection, dbPath)))
                {
                    c.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, c);
                    cmd.ExecuteNonQuery();
                }
                Info = $"Row added";
                DbInfo = GetDbInfo(dbPath);
            }
            catch (Exception ex)
            {
                Info = $"{MethodBase.GetCurrentMethod().Name}: {ex.Message}";
            }
        }

        private void DbGetData()
        {
            string s = "Name(s): ";
            try
            {
                string sql = "SELECT * FROM People order by Name";
                DataTable dt = new DataTable();
                using (var c = new SqliteConnection(string.Format(dbConnection, dbPath)))
                {
                    c.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, c);
                    SqliteDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string t = dr["Name"].ToString();
                    s += $"{t} ";
                }
            }
            catch (Exception ex)
            {
                Info = $"{MethodBase.GetCurrentMethod().Name}: {ex.Message}";
            }
            Info = s;
        }

        private string GetDbInfo(string dbPath)
        {
            // Does file exist?
            if (File.Exists(dbPath))
            {
                string s = $"File '{dbName}' exists. ";
                string sql = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";
                DataTable dt = new DataTable();
                using (var c = new SqliteConnection(string.Format(dbConnection, dbPath)))
                {
                    c.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, c);
                    SqliteDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                }
                s += "Table(s): ";
                foreach (DataRow dr in dt.Rows)
                {
                    string t = dr[0].ToString();
                    s += $"{t} ";
                    using (var c = new SqliteConnection(string.Format(dbConnection, dbPath)))
                    {
                        c.Open();
                        SqliteCommand cmd = new SqliteCommand($"select count(*) from {t}", c);
                        var n = cmd.ExecuteScalar();
                        s += $" [{n} row(s)]";
                    }
                }
                return s;
            }
            else
                return $"File '{dbName}' does not exist";
        }
    }
}
