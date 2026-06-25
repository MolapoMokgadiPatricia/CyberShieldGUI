using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CyberShieldGUI
{
    public class DatabaseService
    {
        private string connectionString =
            "server=localhost;database=CyberShieldDB;uid=root;pwd=Mahlo@230413;";

        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void AddTask(CyberTask task)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                @"INSERT INTO Tasks
                (Title, Description, ReminderDate, IsCompleted)
                VALUES
                (@title, @description, @reminderDate, @completed)";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@reminderDate", task.ReminderDate);
                cmd.Parameters.AddWithValue("@completed", task.IsCompleted);

                cmd.ExecuteNonQuery();
            }
        }

        public List<CyberTask> GetTasks()
        {
            List<CyberTask> tasks =
                new List<CyberTask>();

            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Tasks";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                MySqlDataReader reader =
                    cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new CyberTask
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        ReminderDate = reader["ReminderDate"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["ReminderDate"]),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    });
                }
            }

            return tasks;
        }

        public void DeleteTask(int taskId)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    "DELETE FROM Tasks WHERE Id = @id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", taskId);

                cmd.ExecuteNonQuery();
            }
        }

        public void MarkTaskCompleted(int taskId)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    "UPDATE Tasks SET IsCompleted = TRUE WHERE Id = @id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", taskId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}