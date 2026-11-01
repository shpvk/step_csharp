delegate bool NumberCondition(int number);

class Program
{
    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }
    static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }
        for (int divisor = 2; divisor * divisor <= number; divisor++)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }
        return true;
    }
    static bool IsFibonacci(int number)
    {
        int current = 0;
        int next = 1;
        while (current < number)
        {
            int sum = current + next;
            current = next;
            next = sum;
        }
        return current == number;
    }

    static int[] Filter(int[] numbers, NumberCondition condition)
    {
        int resultLength = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (condition(numbers[i]))
            {
                resultLength++;
            }
        }
        int[] result = new int[resultLength];
        int index = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (condition(numbers[i]))
            {
                result[index] = numbers[i];
                index++;
            }
        }
        return result;
    }

    static void PrintArray(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i] + " ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 21, 34, 35 };
        NumberCondition[] conditions = { IsEven, IsOdd, IsPrime, IsFibonacci };
        Console.Write("Array: ");
        PrintArray(numbers);
        int choice = 0;
        while (choice != 5)
        {
            Console.Write("\n1 Even\n2 Odd\n3 Prime\n4 Fibonacci\n5 Exit\nYour choice: ");
            choice = int.Parse(Console.ReadLine());
            if (choice >= 1 && choice <= 4)
            {
                NumberCondition selectedCondition = conditions[choice - 1];
                int[] result = Filter(numbers, selectedCondition);
                PrintArray(result);
            }
        }
    }
}
