using System.Configuration;
using ConsoleTableExt;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker;

public class UserInput
{
    private static string TIME = ConfigurationManager.AppSettings["time"];
    private static string DATE = ConfigurationManager.AppSettings["date"];
    private static string connectionString = @"Data Source = coding_tracker.db";
    public static bool closeApp = false;    
    public static void Input()
    {
        while (closeApp == false)
        {
            Console.Clear();
            
            Console.WriteLine("\nMenu");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("\ntype 0 if you want to close application");
            Console.WriteLine("Type 1 to View Coding records");
            Console.WriteLine("Type 2 to insert Coding Hours");
            Console.WriteLine("Type 3 to delete record Coding Hours");
            Console.WriteLine("Type 4 to update record");
            Console.WriteLine("\n--------------------------------------------");

            string command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    ViewRecords();
                    Console.WriteLine("Press any key to return to menu:");
                    Console.ReadLine();
                    break;
                case "2":
                    InsertCodingRecords();
                    break;
                case "3":
                    DeleteRecords();
                    break;
                case "4":
                    UpdateRecords();
                    break;
            }
        }
    }

    private static void ViewRecords()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM coding_tracker ";

            var tableData = new List<List<object>> {};

            SqliteDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    List<object> rowData = new List<object>
                    {
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetTimeSpan(4)
                    };
                    
                    tableData.Add(rowData);
                }
            }
            else
            {
                Console.WriteLine("No data found!");
            }
            Console.Clear();

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Coding Tracker ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                .WithColumn("Id", "Date", "Start Time", "End Time", "Duration")
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine(TableAligntment.Center);
        }
    }

    private static void UpdateRecords()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            ViewRecords();
            
            Console.WriteLine("Enter the id of the row you want to update");
            
            string id = Console.ReadLine();
            int Id;

            while (!int.TryParse(id, out Id))
            {
                Console.WriteLine("Id has to be an integer!");
                id = Console.ReadLine();
            }

            string date = GetDateInput();

            DateTime startTime = GetTimeFromUser($"Enter the new start time({TIME}):");

            DateTime endTime = GetTimeFromUser($"Enter the new end time({TIME}):");

            TimeSpan duration = endTime - startTime;
            
            var tableCmd = connection.CreateCommand();

            string startTimeFinal = startTime.ToString($"{TIME}");

            string endTimeFinal = endTime.ToString($"{TIME}");
            

            tableCmd.CommandText = $"UPDATE coding_tracker SET Date = '{date}', StartTime= '{startTimeFinal}',EndTime = '{endTimeFinal}',Duration = '{duration}' WHERE Id = '{Id}'";

            tableCmd.ExecuteNonQuery();
            
            connection.Close();
            
        }
    }

    private static void DeleteRecords()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            ViewRecords();
            
            Console.WriteLine("Enter the id of the row you want to delete:");
            string id = Console.ReadLine();
            int Id;

            while (!int.TryParse(id, out Id))
            {
                Console.WriteLine("Id has to be an integer!");
                id = Console.ReadLine();
            }

            connection.Open();
                                                                                 
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"DELETE FROM coding_tracker WHERE Id = '{Id}'";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    private static void InsertCodingRecords()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            ViewRecords();
            
            string date = GetDateInput();

            DateTime startTime = GetTimeFromUser($"Enter start time({TIME})->");

            DateTime endTime = GetTimeFromUser($"Enter end time({TIME})->");

            TimeSpan duration = endTime - startTime;
            
            var tableCmd = connection.CreateCommand();
            
            string startTimeFinal = startTime.ToString($"{TIME}");

            string endTimeFinal = endTime.ToString($"{TIME}");

            tableCmd.CommandText = $"INSERT INTO coding_tracker(Date, StartTime, EndTime, Duration) VALUES('{date}', '{startTimeFinal}', '{endTimeFinal}', '{duration}')";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    private static string GetDateInput()
    {
        Console.WriteLine($"Enter the date({DATE}):");

        DateTime dt = Validation.DateInputValidation($"{DATE}");

        string result = dt.ToString($"{DATE}");

        return result;
    }

    private static DateTime GetTimeFromUser(string prompt)
    {
        Console.Write(prompt);

        DateTime result = Validation.DateInputValidation($"{TIME}");

        return result;
    }
}