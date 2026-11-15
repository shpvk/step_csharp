class Program
{
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

    static void Main()
    {
        try
        {
            Random random = new Random();
            int primeCount = 0;
            int fibonacciCount = 0;
            using (StreamWriter primeWriter = new StreamWriter("primes.txt"))
            using (StreamWriter fibonacciWriter = new StreamWriter("fibonacci.txt"))
            {
                for (int i = 0; i < 100; i++)
                {
                    int number = random.Next(0, 101);
                    if (IsPrime(number))
                    {
                        primeWriter.WriteLine(number);
                        primeCount++;
                    }
                    if (IsFibonacci(number))
                    {
                        fibonacciWriter.WriteLine(number);
                        fibonacciCount++;
                    }
                }
            }
            Console.WriteLine("Generated numbers: 100");
            Console.WriteLine("Prime numbers saved to primes.txt: " + primeCount);
            Console.WriteLine("Fibonacci numbers saved to fibonacci.txt: " + fibonacciCount);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
