namespace Coding_Tracker;

public class Validation
{
    public static DateTime DateInputValidation(string format)
    {
        DateTime result;
        bool isValidInput;
        string userInput = Console.ReadLine();
        do
        {
            isValidInput = DateTime.TryParseExact(userInput, $"{format}", null, System.Globalization.DateTimeStyles.None,
                out result);

            if (!isValidInput)
            {
                Console.WriteLine($"Invalid input. Please enter a valid time-{format}:");
                userInput = Console.ReadLine();
            }
        } while (!isValidInput);

        return result;
    }
}