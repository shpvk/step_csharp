using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string input = "Valid: alex@gmail.com, john.doe@yahoo.com, maria_123@outlook.com, " +
            "test-user@company.ua, admin123@mail.co.uk. " +
            "Invalid: alexgmail.com, john@, @gmail.com, alex@gmail, alex@gmail.c.";

        string pattern = @"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}\b";
        Regex regex = new Regex(pattern);

        Match match = regex.Match(input);
        while (match.Success)
        {
            Console.WriteLine(match.Value);
            match = match.NextMatch();
        }
    }
}
