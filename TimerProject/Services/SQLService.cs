using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerProject.Models.SQLModels;

namespace TimerProject.Services
{
    public class SQLService
    {
        public const string DATABASENAME = "Project.db3";
       
        public bool CreateDataBase()
        {
            bool isCreate=false;
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), DATABASENAME)))
            {
                SQLiteConnection.CreateFile(DATABASENAME);
                CreateTables();
                isCreate = true;
                return isCreate;
            }
            return isCreate;
        }
        public void CreateTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DATABASENAME};Version=3;"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                command.CommandText = "CREATE TABLE IF NOT EXISTS Projects (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS TimeWork (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                                            "DateStart TEXT, DateEnd TEXT,idProject INTEGER)";
                command.ExecuteNonQuery();
            }
        }
        public void InsertProjectData(Project item)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DATABASENAME};Version=3;"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                command.CommandText = @"INSERT INTO Projects (name) VALUES('INTERCAKE')";
                command.ExecuteNonQuery();
            }
        }
        public void InsertTimeWorkData(TimeWork dateWork)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DATABASENAME};Version=3;"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                command.CommandText = $"INSERT INTO TimeWork (DateStart,DateEnd,idProject) VALUES('{dateWork.DateStart.ToString()}','{dateWork.DateEnd.ToString()}','{dateWork.IdProject}')";
                command.ExecuteNonQuery();
            }
        }
        public void DeleteData() { }
        public string GetHours(Project project)
        {
            DataTable dTable = new DataTable();
            int hours=default;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DATABASENAME};Version=3;"))
            {
                string sqlQuery = $"SELECT TimeWork.DateStart,TimeWork.DateEnd FROM TimeWork WHERE TimeWork.IdProject={project.Id}";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, connection);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                    foreach (DataRow item in dTable.Rows)
                    {
                        string tmp = (Convert.ToDateTime(item.ItemArray[1]).Subtract(Convert.ToDateTime(item.ItemArray[0]))).ToString();
                        hours += Convert.ToInt32(tmp.Substring(0,2));
                    }
            }
            return hours.ToString();
        }
        public List<Project> GetProject()
        {
            DataTable dTable = new DataTable();
            List<Project> list = new List<Project>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DATABASENAME};Version=3;"))
            {
                string sqlQuery = $"SELECT * FROM Projects";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, connection);
                adapter.Fill(dTable);

                if (dTable.Rows.Count>0)
                {
                    foreach (DataRow item in dTable.Rows)
                    {
                        Project x = new Project();
                        x.Id = Convert.ToInt32(item.ItemArray[0].ToString());
                        x.Name = item.ItemArray[1].ToString();
                        list.Add(x);
                    }
                }

            }
                return list;
        }


    }
}
