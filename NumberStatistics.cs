class Program
{
    static void GenerateNumbersFile(string path, int amount)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int i = 0; i < amount; i++)
            {
                writer.WriteLine(random.Next(-99999, 100000));
            }
        }
    }

    static void Main()
    {
        try
        {
            string path = "numbers.txt";
            if (!File.Exists(path))
            {
                GenerateNumbersFile(path, 100000);
                Console.WriteLine("Created " + path + " with 100000 numbers");
            }

            int positiveCount = 0;
            int negativeCount = 0;
            int twoDigitCount = 0;
            int fiveDigitCount = 0;
            using (StreamReader reader = new StreamReader(path))
            using (StreamWriter positiveWriter = new StreamWriter("positive.txt"))
            using (StreamWriter negativeWriter = new StreamWriter("negative.txt"))
            using (StreamWriter twoDigitWriter = new StreamWriter("two_digit.txt"))
            using (StreamWriter fiveDigitWriter = new StreamWriter("five_digit.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    int number = int.Parse(line);
                    int absolute = Math.Abs(number);
                    if (number > 0)
                    {
                        positiveWriter.WriteLine(number);
                        positiveCount++;
                    }
                    if (number < 0)
                    {
                        negativeWriter.WriteLine(number);
                        negativeCount++;
                    }
                    if (absolute >= 10 && absolute <= 99)
                    {
                        twoDigitWriter.WriteLine(number);
                        twoDigitCount++;
                    }
                    if (absolute >= 10000 && absolute <= 99999)
                    {
                        fiveDigitWriter.WriteLine(number);
                        fiveDigitCount++;
                    }
                    line = reader.ReadLine();
                }
            }

            Console.WriteLine("Positive numbers: " + positiveCount);
            Console.WriteLine("Negative numbers: " + negativeCount);
            Console.WriteLine("Two-digit numbers: " + twoDigitCount);
            Console.WriteLine("Five-digit numbers: " + fiveDigitCount);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
