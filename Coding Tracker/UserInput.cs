using System.Data;

namespace Coding_Tracker;

public class UserInput
{
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
                //case "1":
                    //ViewRecords();
                    //break;
                //case "2":
                    //InsertRecords();
                    //break;
                //case "3":
                    //DeleteRecords();
                    //break;
                //case "4":
                    //UpdateRecords();
                    //break;
            }
        }
    }
}