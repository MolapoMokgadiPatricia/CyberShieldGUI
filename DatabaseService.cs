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

        // FIX 1: Returns the new row's Id so TaskManager can store it back
        // on the CyberTask object — needed for CompleteTask and DeleteTask to work
        public int AddTask(CyberTask task)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                @"INSERT INTO Tasks
                (Title, Description, ReminderDate, IsCompleted)
                VALUES
                (@title, @description, @reminderDate, @completed);
                SELECT LAST_INSERT_ID();";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@reminderDate", (object?)task.ReminderDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@completed", task.IsCompleted);

                return Convert.ToInt32(cmd.ExecuteScalar());
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

                string query = "DELETE FROM Tasks WHERE Id = @id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.ExecuteNonQuery();
            }
        }

        // FIX 2: Use 1 instead of TRUE for reliable boolean update in MySQL
        public void MarkTaskCompleted(int taskId)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = @id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.ExecuteNonQuery();
            }
        }

        // FIX 3: New method to update the reminder date for a task
        public void UpdateReminder(int taskId, DateTime reminderDate)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    "UPDATE Tasks SET ReminderDate = @reminderDate WHERE Id = @id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@reminderDate", reminderDate);
                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
