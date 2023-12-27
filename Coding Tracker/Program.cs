using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    class Program
    {
        private static string connectionString = @"Data Source = coding_tracker.db";

        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                
                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS coding_tracker(
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Date TEXT, 
                                        CodingHours INTEGER)";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
            UserInput.Input();
        }
    }
}