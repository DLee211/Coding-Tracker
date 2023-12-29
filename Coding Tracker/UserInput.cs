using System.Data;
using ConsoleTableExt;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker;

public class UserInput
{
    private static string connectionString = @"Data Source = coding_tracker.db";
    public static bool closeApp = false;    
    public static void Input()
    {
        while (closeApp == false)
        {
            Console.WriteLine("\nMenu");
            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("\ntype 0 if you want to close application");
            Console.WriteLine("Type 1 to View Coding records");
            Console.WriteLine("Type 2 to insert Coding Hours");
            Console.WriteLine("Type 3 to delete record Coding Hours");
            Console.WriteLine("Type 4 to update record");
            Console.WriteLine("--------------------------------------------");

            string command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    ViewRecords();
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
                        reader.GetDateTime(1),
                        reader.GetDateTime(2),
                        reader.GetDateTime(3),
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
                .ExportAndWriteLine();
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

            DateTime date = GetDateInput();

            DateTime startTime = GetTimeFromUser("Enter the new start time(HH:mm):");

            DateTime endTime = GetTimeFromUser("Enter the new end time(HH:mm):");

            TimeSpan duration = endTime - startTime;
            
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"UPDATE coding_tracker SET Date = '{date}', StartTime= '{startTime}',EndTime = '{endTime}',Duration = '{duration}' WHERE Id = '{Id}'";

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
            
            DateTime date = GetDateInput();

            DateTime startTime = GetTimeFromUser("Enter start time(hh:mm)->");

            DateTime endTime = GetTimeFromUser("Enter end time(HH:MM)->");

            TimeSpan duration = endTime - startTime;
            
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"INSERT INTO coding_tracker(Date, StartTime, EndTime, Duration) VALUES('{date}', '{startTime}', '{endTime}', '{duration}')";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    private static DateTime GetDateInput()
    {
        Console.WriteLine("Enter the date(MM/dd/yyyy):");

        DateTime result;

        bool isValidInput;

        do
        {

            string date = Console.ReadLine();

            isValidInput = DateTime.TryParseExact(date, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None,
                out result);
            if (!isValidInput)
            {
                Console.WriteLine("Invalid date");
            }

        } while (!isValidInput);

        return result;

    }

    private static DateTime GetTimeFromUser(string prompt)
    {
        DateTime result;
        bool isValidInput;

        do
        {
            Console.Write(prompt);
            string userInput = Console.ReadLine();

            isValidInput = DateTime.TryParseExact(userInput, "HH:mm", null, System.Globalization.DateTimeStyles.None,
                out result);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter a valid time");
            }
        } while (!isValidInput);

        return result;
    }
}

internal class CodingTracker
{
    public int Id;
    public DateTime Date;
    public DateTime StartTime;
    public DateTime EndTime;
    public TimeSpan Duration;
}