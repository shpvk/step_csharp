class Program
{
    static void Swap<T>(ref T first, ref T second)
    {
        T temporary = first;
        first = second;
        second = temporary;
    }

    static void Main()
    {
        int firstNumber = 5;
        int secondNumber = 10;
        Console.WriteLine("Before: " + firstNumber + " " + secondNumber);
        Swap(ref firstNumber, ref secondNumber);
        Console.WriteLine("After: " + firstNumber + " " + secondNumber);

        string firstWord = "apple";
        string secondWord = "banana";
        Console.WriteLine("Before: " + firstWord + " " + secondWord);
        Swap(ref firstWord, ref secondWord);
        Console.WriteLine("After: " + firstWord + " " + secondWord);

        double firstValue = 1.5;
        double secondValue = 2.5;
        Console.WriteLine("Before: " + firstValue + " " + secondValue);
        Swap<double>(ref firstValue, ref secondValue);
        Console.WriteLine("After: " + firstValue + " " + secondValue);
    }
}
