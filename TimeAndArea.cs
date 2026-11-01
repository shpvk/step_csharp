class Program
{
    static void ShowTime()
    {
        Console.WriteLine("Time: " + DateTime.Now.ToString("HH:mm:ss"));
    }
    static void ShowDate()
    {
        Console.WriteLine("Date: " + DateTime.Now.ToString("dd.MM.yyyy"));
    }
    static void ShowDayOfWeek()
    {
        Console.WriteLine("Day of week: " + DateTime.Now.DayOfWeek);
    }
    static double TriangleArea(double baseLength, double height)
    {
        return baseLength * height / 2;
    }
    static double RectangleArea(double width, double height)
    {
        return width * height;
    }
    static bool IsPositive(double number)
    {
        return number > 0;
    }

    static void Main()
    {
        Action[] showActions = { ShowTime, ShowDate, ShowDayOfWeek };
        Func<double, double, double>[] areaFunctions = { TriangleArea, RectangleArea };
        Predicate<double> isValidSize = IsPositive;
        int choice = 0;
        while (choice != 6)
        {
            Console.Write("\n1 Time\n2 Date\n3 Day of week\n4 Triangle area\n5 Rectangle area\n6 Exit\nYour choice: ");
            choice = int.Parse(Console.ReadLine());
            if (choice >= 1 && choice <= 3)
            {
                Action selectedAction = showActions[choice - 1];
                selectedAction();
            }
            else if (choice == 4 || choice == 5)
            {
                try
                {
                    Console.Write("Enter length: ");
                    double length = double.Parse(Console.ReadLine());
                    Console.Write("Enter height: ");
                    double height = double.Parse(Console.ReadLine());
                    if (!isValidSize(length) || !isValidSize(height))
                    {
                        throw new Exception("Length and height must be positive");
                    }
                    Func<double, double, double> selectedArea = areaFunctions[choice - 4];
                    Console.WriteLine("Area: " + selectedArea(length, height));
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }
    }
}
